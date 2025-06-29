using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UTeleApp.Demo
{
    [Serializable]
    public struct CreateInvoiceLinkEventArgs
    {
        public bool ok;
        public string result;
    }

    [Serializable]
    public class UpdateResponse
    {
        public bool ok;
        public Update[] result;
    }

    [Serializable]
    public class Update
    {
        public long update_id;
        public PreCheckoutQuery pre_checkout_query;
        public Message message;
    }

    [Serializable]
    public class PreCheckoutQuery
    {
        public string id;
        public User from;
        public string currency;
        public int total_amount;
        public string invoice_payload;
    }

    [Serializable]
    public class User
    {
        public long id;
        public bool is_bot;
        public string first_name;
        public string last_name;
        public string username;
    }

    [Serializable]
    public class Message
    {
        public SuccessfulPayment successful_payment;
    }

    [Serializable]
    public class SuccessfulPayment
    {
        public string currency;
        public int total_amount;
        public string invoice_payload;
        public string telegram_payment_charge_id;
        public string provider_payment_charge_id;
    }

    [Serializable]
    public class PrepareMessageResponseResult
    {
        public string id;
        public int expiration_date;
    }

    [Serializable]
    public class PrepareMessageResponse
    {
        public bool ok;
        public PrepareMessageResponseResult result; // This will contain the message_id
    }

    [Serializable]
    public class InlineQueryResultArticle : InlineQueryResult
    {
        public string url; // Optional. URL of the result
        public bool hide_url; // Optional. Pass True if you don't want the URL to be shown
        public string description; // Optional. Short description of the result
        public string thumbnail_url; // Optional. Url of the thumbnail for the result
        public int thumbnail_width; // Optional. Thumbnail width
        public int thumbnail_height; // Optional. Thumbnail height

        public InlineQueryResultArticle()
        {
            type = "article";
        }
    }

    [Serializable]
    public class InlineQueryResult
    {
        public string type; // Type of the result
        public string id; // Unique identifier for this result
        public string title; // Title of the result
        public InputMessageContent input_message_content;
        public InlineKeyboardMarkup reply_markup;
    }

    [Serializable]
    public class InputMessageContent
    {
        public string message_text; // Text of the message
        public string parse_mode; // Optional. "HTML" or "Markdown"
    }

    [Serializable]
    public class InlineKeyboardMarkup
    {
        public InlineKeyboardButton[][] inline_keyboard;
    }

    [Serializable]
    public class InlineKeyboardButton
    {
        public string text;
        public string url;
    }

    // below example is POC for the payment/share message functions, using client restful api for bot interaction, we recommand to use backend/server program to run bot api
    public class PaymentAndShareMsgDemoController : BaseDemoController
    {
        [SerializeField]
        private Button _openInvoiceBtn;

        [SerializeField]
        private Button _shareMessageBtn;

        [SerializeField]
        private Text _openInvoiceResultText;

        [SerializeField]
        private Text _shareMessageResultText;

        private string botToken = "your_bot_token";
        private long _lastUpdateId = 0; // Track the last update ID we've processed

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            return;
#endif
            if (botToken == "your_bot_token")
            {
                Debug.LogWarning("bot token invalid, please get it from @BotFather");
                return;
            }
            else
            {
                Debug.LogWarning(
                    "below example is POC for the payment/share message functions, using client restful api for bot interaction, we recommand to use backend/server program to run bot api"
                );
            }

            _openInvoiceBtn.onClick.AddListener(() =>
            {
                StartCoroutine(
                    GenerateInvoice(
                        (linkUrl) =>
                        {
                            TelegramWebApp.OpenInvoice(linkUrl);
                        }
                    )
                );
            });

            _shareMessageBtn.onClick.AddListener(() =>
            {
                StartCoroutine(PrepareAndShareMessage());
            });
        }

        private void OnEnable()
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            return;
#endif
            if (botToken == "your_bot_token")
            {
                Debug.LogWarning("bot token invalid, please get it from @BotFather");
                return;
            }
            // Start polling for updates
            StartCoroutine(PollUpdates());

            // Register event handlers
            TelegramWebAppEvents.OnInvoiceClosed += HandleInvoiceClosed;
            TelegramWebAppEvents.OnShareMessageSent += HandleShareMessageSent;
            TelegramWebAppEvents.OnShareMessageFailed += HandleShareMessageFailed;
        }

        private void OnDisable()
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            return;
#endif
            TelegramWebAppEvents.OnInvoiceClosed -= HandleInvoiceClosed;
            TelegramWebAppEvents.OnShareMessageSent -= HandleShareMessageSent;
            TelegramWebAppEvents.OnShareMessageFailed -= HandleShareMessageFailed;
        }

        private IEnumerator PollUpdates()
        {
            while (true)
            {
                yield return StartCoroutine(GetUpdates());
                yield return new WaitForSeconds(1f); // Poll every second
            }
        }

        private IEnumerator GetUpdates()
        {
            Debug.Log($"Fetching Update {_lastUpdateId}");
            string url = $"https://api.telegram.org/bot{botToken}/getUpdates";
            if (_lastUpdateId > 0)
            {
                url += $"?offset={_lastUpdateId + 1}";
            }

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string response = www.downloadHandler.text;
                    if (string.IsNullOrEmpty(response))
                    {
                        Debug.LogError("Received empty response from Telegram API");
                        yield break;
                    }

                    Debug.Log($"Raw response: {response}");

                    UpdateResponse updates = JsonUtility.FromJson<UpdateResponse>(response);

                    if (updates.ok && updates.result != null && updates.result.Length > 0)
                    {
                        foreach (Update update in updates.result)
                        {
                            // Log each update for debugging
                            Debug.Log($"Processing update ID: {update.update_id}");

                            if (update.pre_checkout_query != null)
                            {
                                Debug.Log(
                                    $"Found pre_checkout_query: {update.pre_checkout_query.id}"
                                );
                                if (!string.IsNullOrEmpty(update.pre_checkout_query.id))
                                {
                                    yield return StartCoroutine(
                                        HandlePreCheckoutQuery(update.pre_checkout_query.id, true)
                                    );
                                }
                            }

                            if (update.message != null)
                            {
                                Debug.Log(
                                    $"Found message with successful_payment: {update.message.successful_payment != null}"
                                );
                                if (update.message.successful_payment != null)
                                {
                                    Debug.Log(
                                        $"Payment successful! Amount: {update.message.successful_payment.total_amount} {update.message.successful_payment.currency}"
                                    );
                                }
                            }

                            if (update.update_id > _lastUpdateId)
                                _lastUpdateId = update.update_id;
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Error getting updates: {www.error}");
                }
            }
        }

        // Ref: https://core.telegram.org/bots/api#createinvoicelink
        // This is demo for open invoice
        // We don't suggest to generate invoice from client app, the better way is to generate the invoice from the bot app(backend) which is more secure
        private IEnumerator GenerateInvoice(Action<string> callback)
        {
            string invoiceId = Guid.NewGuid().ToString();
            int price = 1;

            WWWForm form = new WWWForm();
            form.AddField("title", "Test Item"); // Product name, 1-32 characters
            form.AddField("description", "Purchase the item with Telegram Stars!"); // Product description, 1-255 characters
            form.AddField("payload", "{\"id\": \"" + invoiceId + "\"}"); //	Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use it for your internal processes.
            form.AddField("currency", "XTR"); // XTR for payments in Telegram Stars
            form.AddField("prices", "[{\"label\":\"Test Item Price\",\"amount\":" + price + "}]"); // Price breakdown, a JSON-serialized list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in Telegram Stars.

            UnityWebRequest www = UnityWebRequest.Post(
                $"https://api.telegram.org/bot{botToken}/createInvoiceLink",
                form
            );
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Payment Success");
                string text = www.downloadHandler.text;
                CreateInvoiceLinkEventArgs returnResult =
                    JsonUtility.FromJson<CreateInvoiceLinkEventArgs>(text);
                Debug.Log(text);
                callback(returnResult.result);
            }
            else
            {
                Debug.LogError("Error: " + www.downloadHandler.text);
            }
        }

        // Ref: https://core.telegram.org/bots/api#answerprecheckoutquery
        // You need to answer pre check out query from bot to get successful payment return
        // We don't suggest to do it from client app, the better way is to generate the invoice from the bot app(backend) which is more secure
        private IEnumerator HandlePreCheckoutQuery(
            string preCheckoutQueryId,
            bool ok,
            string errorMessage = null
        )
        {
            WWWForm form = new WWWForm();
            form.AddField("pre_checkout_query_id", preCheckoutQueryId);
            form.AddField("ok", ok.ToString().ToLower());

            if (!ok && !string.IsNullOrEmpty(errorMessage))
            {
                form.AddField("error_message", errorMessage);
            }

            UnityWebRequest www = UnityWebRequest.Post(
                $"https://api.telegram.org/bot{botToken}/answerPreCheckoutQuery",
                form
            );
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Pre-checkout query answered successfully");
            }
            else
            {
                Debug.LogError("Error answering pre-checkout query: " + www.downloadHandler.text);
            }
        }

        private IEnumerator PrepareAndShareMessage()
        {
            _shareMessageResultText.text = "Preparing message...";

            // Create the inline query result
            InlineQueryResultArticle inlineResult = new InlineQueryResultArticle
            {
                id = Guid.NewGuid().ToString(),
                type = "article",
                title = "Check out this Mini App!",
                description = "An awesome Telegram Mini App experience, Built with UTeleApp!",
                url =
                    "https://assetstore.unity.com/packages/tools/integration/uteleapp-tools-for-telegram-mini-apps-300734",
                hide_url = false,
                input_message_content = new InputMessageContent
                {
                    message_text =
                        "Join me in this amazing Mini App! 🚀\n\n"
                        + "🔗 <a href=\"https://assetstore.unity.com/packages/tools/integration/uteleapp-tools-for-telegram-mini-apps-300734\">UTeleApp on Unity Asset Store</a>",
                    parse_mode = "HTML",
                },
            };

            WWWForm form = new WWWForm();
            form.AddField("user_id", TelegramWebApp.InitDataUnsafe.user.id.ToString()); // Get user ID from InitData
            form.AddField("result", JsonUtility.ToJson(inlineResult));
            form.AddField("allow_user_chats", "true");
            form.AddField("allow_bot_chats", "true");
            form.AddField("allow_group_chats", "true");
            form.AddField("allow_channel_chats", "true");

            UnityWebRequest www = UnityWebRequest.Post(
                $"https://api.telegram.org/bot{botToken}/savePreparedInlineMessage",
                form
            );

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;
                Debug.Log($"Prepare message response: {response}");
                PrepareMessageResponse prepareResponse =
                    JsonUtility.FromJson<PrepareMessageResponse>(response);

                if (prepareResponse.ok)
                {
                    Debug.Log($"Sharing message with ID: {prepareResponse.result}");
                    _shareMessageResultText.text = "Sharing message...";
                    TelegramWebApp.ShareMessage(prepareResponse.result.id);
                }
                else
                {
                    Debug.LogError("Failed to prepare message");
                    _shareMessageResultText.text = "Failed to prepare message";
                }
            }
            else
            {
                string errorMessage = $"Error preparing message: {www.error}";
                Debug.LogError(errorMessage);
                _shareMessageResultText.text = "Error preparing message";
            }
        }

        private void HandleShareMessageSent()
        {
            Debug.Log("Message shared successfully!");
            _shareMessageResultText.text = "Message shared successfully!";
        }

        private void HandleShareMessageFailed(ShareMessageFailedEventArgs args)
        {
            Debug.LogError($"Share message failed: {args.error}");
            _shareMessageResultText.text = $"Share failed: {args.error}";
        }

        private void HandleInvoiceClosed(InvoiceClosedEventArgs args)
        {
            Debug.Log($"Invoice closed with status: {args.status}");

            switch (args.status)
            {
                case "paid":
                    _openInvoiceResultText.text = "Payment successful!";
                    break;

                case "failed":
                    _openInvoiceResultText.text = "Payment failed";
                    break;

                case "cancelled":
                    _openInvoiceResultText.text = "Payment cancelled";
                    break;

                case "pending":
                    _openInvoiceResultText.text = "Payment pending...";
                    break;

                default:
                    _openInvoiceResultText.text = $"Invoice closed: {args.status}";
                    break;
            }
        }
    }
}
