using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBusinessCentralDemo.LoginCredentials
{
    public class LoginPage
    {
        private IPage _page;

        private readonly ILocator _username;
        private readonly ILocator _password;
        private readonly ILocator _clickButton; 

        public LoginPage(IPage page)
        {
            _page = page;

            _username = _page.Locator("[name='loginfmt']");
            _password = _page.Locator("[name='passwd']");
            _clickButton = _page.Locator("[id='idSIButton9']");

        }
        public async Task InputLoginCredentialsUsername(string userName)
        {
            await _username.FillAsync(userName);
        }
        public async Task InputLoginCredentialsPassword(string password)
        {
            await _password.FillAsync(password);
        }

        public async Task ClickLogin() => await _clickButton.ClickAsync();

    }
}
