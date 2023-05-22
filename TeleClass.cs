using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsApp2.Models;

namespace WinFormsApp2
{
    public class TeleClass
    {
        string token = "6206692530:AAFHXhsD_N4MUsVKiNXtiWdwJaECSRmTMss";
        string BaseUrl = "https://api.telegram.org/bot";
        WebClient client;
        int LastUpdateId = 0;
        TextBox txtLog;
        public TeleClass(TextBox tLog) { 
            this.txtLog = tLog;
            Init();
        }
        private void Init()
        {
            client = new WebClient();
        

        }
        public void GetUpdates()
        {
            //string address = BaseUrl + token + "/getMe";
            string address = BaseUrl + token + "/getUpdates?offset=" + (LastUpdateId + 1);
            string str = client.DownloadString(address);
            TeleMessage msg = JsonSerializer.Deserialize<TeleMessage>(str);
            if (!msg.ok || msg.result.Length == 0) { return; }
            foreach (var item in msg.result)
            {
                LastUpdateId = item.update_id;
                if (item.message != null)
                {
                    AnswerIsMessage(item);
                }
                if (item.callback_query != null)
                {
                    AnswerIsQuery(item);
                }

            }

        }
      
        private void AnswerIsMessage(Result item)
        {
            WriteLog(item.message.text);
            SendAnswer(item.message.chat.id, item.message.text);
        }

        private void WriteLog(string str)
        {
            txtLog.Text += DateTime.Now + " " + str + Environment.NewLine;
        }           

        private void MyMenu(long chat_id)
        {
            string address = BaseUrl + token + "/sendMessage";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("chat_id", chat_id.ToString());
            nvc.Add("text", "проверка меню");
            List<string> keyboardBtn1 = new List<string>() {
                "/start",
                "/help"
            };           
            List<List<string>> keyboard = new List<List<string>>
            {
                keyboardBtn1
            };
            TeleButtons buttons = new TeleButtons(keyboard);          
            string replyMarkup = JsonSerializer.Serialize(buttons);
            nvc.Add("reply_markup", replyMarkup);
            client.UploadValues(address, nvc);
        }

        private void SendMessage(long chat_id, string message, string replyMarkup = "")
        {
            string address = BaseUrl + token + "/sendMessage";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("chat_id", chat_id.ToString());
            nvc.Add("text", message);
            if (replyMarkup != null)
            {
                nvc.Add("reply_markup", replyMarkup);
            }
            client.UploadValues(address, nvc);
        }
   
      
        private void ChangeMessage(Result result,string message, string replyMarkup = "")
        {
            string address = BaseUrl + token + "/editMessageText";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("chat_id", result.callback_query.message.chat.id.ToString());
            nvc.Add("message_id", result.callback_query.message.message_id.ToString());
            nvc.Add("text", message);
            if (replyMarkup != null)
            {
                nvc.Add("reply_markup", replyMarkup);
            }
            client.UploadValues(address, nvc);
        }
        //new method
        private string MenuFromDB(string user,out string replyMarkup)
        {
            using (SkladContext context = new SkladContext())
            {
                context.Categories.Load();
                var categories = context.Categories.Select(t => new { t.CategoryName }).Distinct().ToList();
                InlineKeyboard keyboard = new InlineKeyboard();
                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                int i = 0;
                foreach (var category in categories)
                {
                    keyboard.AddButton(new InlineKeyboardButton(category.CategoryName.ToString()), i++ / 2);
                }
                AddCard(keyboard, "", "", user);
                AddMainMenu(keyboard);
                replyMarkup = keyboard.ReturnReplymarkup();
                return "усі категорії товару:";
            }
        }

        private void AddMainMenu(InlineKeyboard keyboard)
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>() {
                new InlineKeyboardButton("Є питання?","?"),
                new InlineKeyboardButton("О нас","about")
            };
           
            keyboard.AddLineButtons(buttons);
        }
        //22.05
        private void AddCard(InlineKeyboard keyboard, string cat_name, string prod_name, string user)
        {
            TeleCard teleCard = new TeleCard(user);
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>() {
                new InlineKeyboardButton($"кошик ({teleCard.Count})","кошик відобразити"),
                new InlineKeyboardButton($"кошик ({teleCard.Count})","кошик змінити")
            };
            if (prod_name != "")
                buttons.Add(new InlineKeyboardButton("Додати в кошик", "кошик купівля " + cat_name + " " + prod_name));
            keyboard.AddLineButtons(buttons);
        }
        private string Shop(string cat_name, string name,string user, out string replyMarkup)
        {
            string answer = "";
            replyMarkup = "";
           using (SkladContext context = new SkladContext())
            {
                context.Products.Load();
                context.Carts.Load();
                var products = context.Products.Where(t=>t.Category.CategoryName==cat_name).Select(t => new { t.Name, t.Category.CategoryName }).ToList();
                if (products.Count == 0) return "товари цієї категорії закінчилися!";
                if(name=="") name= products[0].Name.ToString();
                InlineKeyboard keyboard = new InlineKeyboard();
                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                int i = 0;
                foreach (var product in products)
                {
                    if (name == product.Name.ToString()) continue;
                    keyboard.AddButton(new InlineKeyboardButton(product.Name.ToString(),product.CategoryName.ToString()+" "+product.Name.ToString()), i++ / 3);                 
                }            
               AddCard(keyboard,cat_name, name,user);
                keyboard.AddButton(new InlineKeyboardButton("Назад", "about"));
                AddMainMenu(keyboard);
                //22.05
                replyMarkup = keyboard.ReturnReplymarkup();
                var prod= context.Products.Where(t => t.Category.CategoryName == cat_name).Where(t=>t.Name==name).Select(t => new { t.Name, t.Category.CategoryName, t.Price }).ToList();
                answer= $"{prod[0].Name.ToString()}\n вартість товару: {prod[0].Price.ToString()} грн.";           

            }
            return answer;
        }
        private void SendAnswer(long chat_id, string message)
        {
            string answer = "";
            string replyMarkup = "";
            switch (message.ToLower())
            {
                case "/start":
                    answer = MenuFromDB(chat_id.ToString(),out replyMarkup);
                    break;
                case "/help":
                    answer = "Чем я могу помочь?";
                    break;
                case "меню":
                    MyMenu(chat_id);
                    return;           

                default:
                    answer = $"Вы мне написали {message}, но я не знаю, что ответить!";
                    MenuFromDB(chat_id.ToString(),out replyMarkup);
                    break;
            }
            SendMessage(chat_id, answer, replyMarkup);

        }
        private string Show_cart(Result item, out string replyMarkup) {
            string answer = "";
           replyMarkup = "";
            TeleCard teleCard = new TeleCard(item.callback_query.from.id.ToString());
            string[] _data = item.callback_query.data.Split();
            switch (_data[1] ?? "")
            {
                case "купівля":
                    teleCard.AddProdCart(_data[2], _data[3]);
                    answer = Shop(_data[2], _data[3], item.callback_query.from.id.ToString(), out replyMarkup);
                    break;
                    //22.05
                case "відобразити":
                    answer = teleCard.InfoFromCart();
                    InlineKeyboard keyboard=new InlineKeyboard();
                    List<InlineKeyboardButton> list=new List<InlineKeyboardButton>() { 
                        new InlineKeyboardButton("Оформити","кошик оформити"),
                         new InlineKeyboardButton("Змінити","кошик змінити")
                    };
                    keyboard.AddLineButtons(list);
                    keyboard.AddButton(new InlineKeyboardButton("Назад", "about"));
                    replyMarkup =keyboard.ReturnReplymarkup();
                    break;
                case "змінити":
                    answer = teleCard.ChangeBuy();
                    break;
                default:
                    break;
            }
           return answer;
        }
        private void AnswerIsQuery(Result item)
        {
            string answer = "", replyMarkup = "";
            string[] _data = item.callback_query.data.Split();//напитки
            WriteLog(item.callback_query.data);
            switch (_data[0])
            {
                case "?":
                    answer = "Якщо є питання звертайтеся на поштову адресу!";
                    MenuFromDB(item.callback_query.from.id.ToString(),out replyMarkup);
                    break;
                case "about":
                    answer = "Продуктовий магазин 'АТБ'";
                    MenuFromDB(item.callback_query.from.id.ToString(),out replyMarkup);
                    break;
                case "кошик":
                    answer = Show_cart(item, out replyMarkup);
                    break;
                default:
                    answer = Shop(
                        _data.Length < 1 ? "" : _data[0],
                        _data.Length<2?"":_data[1],
                        item.callback_query.from.id.ToString(),
                        out replyMarkup);
                    break;
            }          
            ChangeMessage(item, answer, replyMarkup);
        }

    }
}
