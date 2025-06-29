/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;

namespace UTeleApp
{
    /// <summary>
    /// This object contains data that is transferred to the Mini App when it is opened. 
    /// It is empty if the Mini App was launched from a keyboard button or from inline mode.
    /// </summary>
    [Serializable]
    public struct WebAppInitData
    {
        /// <summary>
        /// Optional. A unique identifier for the Mini App session, required for sending messages via the answerWebAppQuery method.
        /// </summary>
        public string query_id;

        /// <summary>
        /// Optional. An object containing data about the current user.
        /// </summary>
        public WebAppUser user;

        /// <summary>
        /// Optional. An object containing data about the chat partner of the current user in the chat where the bot was launched via the attachment menu. 
        /// Returned only for private chats and only for Mini Apps launched via the attachment menu.
        /// </summary>
        public WebAppUser receiver;

        /// <summary>
        /// Optional. An object containing data about the chat where the bot was launched via the attachment menu. 
        /// Returned for supergroups, channels and group chats – only for Mini Apps launched via the attachment menu.
        /// </summary>
        public WebAppChat chat;

        /// <summary>
        /// Optional. Type of the chat from which the Mini App was opened. 
        /// Can be either “sender” for a private chat with the user opening the link, “private”, “group”, “supergroup”, or “channel”. 
        /// Returned only for Mini Apps launched from direct links.
        /// </summary>
        public string chat_type;

        /// <summary>
        /// Optional. Global identifier, uniquely corresponding to the chat from which the Mini App was opened. 
        /// Returned only for Mini Apps launched from a direct link.
        /// </summary>
        public string chat_instance;

        /// <summary>
        /// Optional. The value of the startattach parameter, passed via link. 
        /// Only returned for Mini Apps when launched from the attachment menu via link.
        /// The value of the start_param parameter will also be passed in the GET-parameter tgWebAppStartParam, 
        /// so the Mini App can load the correct interface right away.
        /// </summary>
        public string start_param;

        /// <summary>
        /// Optional. Time in seconds, after which a message can be sent via the answerWebAppQuery method.
        /// </summary>
        public int? can_send_after;

        /// <summary>
        /// Unix time when the form was opened.
        /// </summary>
        public int auth_date;

        /// <summary>
        /// A hash of all passed parameters, which the bot server can use to check their validity.
        /// </summary>
        public string hash;

        public string signature;
    }

    /// <summary>
    /// This object contains the data of the Mini App user.
    /// </summary>
    [Serializable]
    public struct WebAppUser
    {
        /// <summary>
        /// A unique identifier for the user or bot. 
        /// This number may have more than 32 significant bits and some programming languages may have difficulty/silent defects in interpreting it. 
        /// It has at most 52 significant bits, so a 64-bit integer or a double-precision float type is safe for storing this identifier.
        /// </summary>
        public long id;

        /// <summary>
        /// Optional. True, if this user is a bot. Returns in the receiver field only.
        /// </summary>
        public bool is_bot;

        /// <summary>
        /// First name of the user or bot.
        /// </summary>
        public string first_name;

        /// <summary>
        /// Optional. Last name of the user or bot.
        /// </summary>
        public string last_name;

        /// <summary>
        /// Optional. Username of the user or bot.
        /// </summary>
        public string username;

        /// <summary>
        /// Optional. IETF language tag of the user's language. Returns in user field only.
        /// </summary>
        public string language_code;

        /// <summary>
        /// Optional. True, if this user is a Telegram Premium user.
        /// </summary>
        public bool is_premium;

        /// <summary>
        /// Optional. True, if this user added the bot to the attachment menu.
        /// </summary>
        public bool added_to_attachment_menu;

        /// <summary>
        /// Optional. True, if this user allowed the bot to message them.
        /// </summary>
        public bool allows_write_to_pm;

        /// <summary>
        /// Optional. URL of the user’s profile photo. 
        /// The photo can be in .jpeg or .svg formats. Only returned for Mini Apps launched from the attachment menu.
        /// </summary>
        public string photo_url;
    }

    /// <summary>
    /// This object represents a chat.
    /// </summary>
    [Serializable]
    public struct WebAppChat
    {
        /// <summary>
        /// Unique identifier for this chat. 
        /// This number may have more than 32 significant bits and some programming languages may have difficulty/silent defects in interpreting it. 
        /// But it has at most 52 significant bits, so a signed 64-bit integer or double-precision float type are safe for storing this identifier.
        /// </summary>
        public long id;

        /// <summary>
        /// Type of chat, can be either “group”, “supergroup” or “channel”.
        /// </summary>
        public string type;

        /// <summary>
        /// Title of the chat.
        /// </summary>
        public string title;

        /// <summary>
        /// Optional. Username of the chat.
        /// </summary>
        public string username;

        /// <summary>
        /// Optional. URL of the chat’s photo. 
        /// The photo can be in .jpeg or .svg formats. Only returned for Mini Apps launched from the attachment menu.
        /// </summary>
        public string photo_url;
    }
}
