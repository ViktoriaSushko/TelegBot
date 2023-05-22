using WinFormsApp2.Models;
namespace WinFormsApp2
{
    public  class TeleCard
    {
        string user;
       public int Count { get; private set; }
       public  TeleCard(string user)
        {
            this.user = user;
            using (SkladContext context = new SkladContext())
            {
               var cards = context.Carts.Where(t=>t.User_name==user).Select(t => new {t.Id, t.User_name, t.Category.CategoryName,t.Product.Name }).ToList();
                Count = cards.Count;
            }

        }
        public void AddProdCart(string nameCat,string prodName)
        {
            using (SkladContext context = new SkladContext())
            {
                var category=context.Categories.FirstOrDefault(t=>t.CategoryName==nameCat);
                var product = context.Products.FirstOrDefault(t => t.Name == prodName);
                Cart cart = new Cart { CategoryId = category.Id, ProductId = product.Id, User_name = user };
                context.Carts.Add(cart);
                context.SaveChanges();
            }
        }
        //22.05
        public void DeleteProdCart(string nameCat, string prodName)
        {
            using (SkladContext context = new SkladContext())
            {
                var category = context.Categories.FirstOrDefault(t => t.CategoryName == nameCat);
                var product = context.Products.FirstOrDefault(t => t.Name == prodName);
                Cart? cart = context.Carts.Where(t=>t.User_name==user).Where(t=>t.CategoryId==category.Id).Where(t=>t.ProductId==product.Id).FirstOrDefault();
                if (cart != null)
                {
                    context.Carts.Remove(cart);
                    context.SaveChanges();
                }               
            }
        }
        public string InfoFromCart()
        {
            string answer = "ваш заказ: \n";
            using (SkladContext context = new SkladContext())
            {
                var products = context.Carts.Select(t => new { t.User_name, t.Category.CategoryName, t.Product.Name, t.Product.Price }).ToList();
                var prodInfo = products.Where(t=>t.User_name==user).GroupBy(t => t.Name).Select(t => new { ProdName = t.Key, Count = t.Count(), Summary = t.Sum(p => p.Price) }).ToList();
                foreach (var pr in prodInfo)
                {
                    answer += $"{pr.ProdName}: {pr.Count} шт. x {pr.Summary} грн.\n\n";
                }
                answer += $"Всього: {prodInfo.Sum(t => t.Count * t.Summary)} грн.";
            }
            return answer;
        }
        //change data
        public string ChangeBuy()
        {
            string answer = "ваш заказ: \n";
            using (SkladContext context = new SkladContext())
            {
                var products = context.Carts.Select(t => new { t.User_name, t.Category.CategoryName, t.Product.Name, t.Product.Price }).ToList();
                var prodInfo = products.Where(t => t.User_name == user).GroupBy(t => t.Name).Select(t => new { ProdName = t.Key, Count = t.Count(), Summary = t.Sum(p => p.Price) }).ToList();
                foreach (var pr in prodInfo)
                {
                    answer += $"{pr.ProdName}: {pr.Count} шт. x {pr.Summary} грн.\n\n";
                }
                answer += $"Всього: {prodInfo.Sum(t => t.Count * t.Summary)} грн.";
            }
            return answer;
        }
    }
}
