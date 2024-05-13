using Microsoft.Playwright;

namespace ShopifyBusinessCentralDemo
{
    internal class ShopifyLoginPage
    {
        private IPage page;

        public ShopifyLoginPage(IPage page)
        {
            this.page = page;
        }
    }
}