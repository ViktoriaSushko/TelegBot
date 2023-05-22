using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    //{"ok":true,"result":{"id":6206692530,"is_bot":true,"first_name":"Test_bot","username":"MyTestSharp_bot","can_join_groups":true,"can_read_all_group_messages":false,"supports_inline_queries":false}}
    public class Result
    {
        public long id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public bool can_join_groups { get; set; }
        public bool can_read_all_group_messages { get; set; }
        public bool supports_inline_queries { get; set; }
        public int update_id { get; set; }
        public Message message { get; set; }
        public Callback_query callback_query { get; set; }
    }
    public class Message
    {
        public int message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public Reply_markup reply_markup { get; set; }
    }
    public class Inline_keyboardItemItem
    {
        public string text { get; set; }
        public string callback_data { get; set; }
    }

    public class Reply_markup
    {
        public List<List<Inline_keyboardItemItem>> inline_keyboard { get; set; }
    }
    public class Chat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    public class From
    {
        public long id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
    }
    public class Callback_query
    {
        public string id { get; set; }
        public From from { get; set; }
        public Message message { get; set; }
        public string chat_instance { get; set; }
        public string data { get; set; }
    }
    public class TeleMessage
    {
        public bool ok { get; set; }
        public Result[] result { get; set; }
    }
}
