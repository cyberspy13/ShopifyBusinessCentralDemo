using Microsoft.Playwright;
using ShopifyBusinessCentralDemo.LoginCredentials;
using System.Diagnostics.CodeAnalysis;

namespace ShopifyBusinessCentralDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {

            using var playwright = await Playwright.CreateAsync();
            // Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }); // Task<IBrowser>
            //Page
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url: "https://businesscentral.dynamics.com/41e8b83d-2266-4629-b144-97c9e432b787/SANDBOX-AZZTEST");

            LoginPage loginPage = new LoginPage(page);
            await Task.Delay(2000);

            string myUsername = "mihail.lecari@azzure.onmicrosoft.com";
            string myPassword = "qu!ckHat70";

            await loginPage.InputLoginCredentialsUsername(userName: myUsername);
            await loginPage.ClickLogin();
            await loginPage.InputLoginCredentialsPassword(password: myPassword);
            await loginPage.ClickLogin();
            await loginPage.ClickLogin();
            /*
            await page.Locator("[name='loginfmt']").FillAsync("email address");
            await page.Locator("[id='idSIButton9']").ClickAsync();
            await page.Locator("[name='passwd']").FillAsync("password");
            await page.Locator("[id='idSIButton9']").ClickAsync();
            await page.Locator("[id='idSIButton9']").ClickAsync();
            */
            await page.GetByRole(AriaRole.Button, new() { NameString = "Search" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { NameString = "Tell me what you want to do" }).FillAsync("Items");
            //await page.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { NameString = "\" / \" Sales Orders Lists Not bookmarked" }).GetByText("Sale  Orders").HoverAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { NameString = "\" / \" Items Lists Not bookmarked" }).GetByText("Items").ClickAsync();
            await page.WaitForURLAsync("https://businesscentral.dynamics.com/41e8b83d-2266-4629-b144-97c9e432b787/SANDBOX-AZZTEST?company=Taskers&page=31&dc=0&bookmark=1B_GwAAAAJ7CDAAMAAwADAANAA5ADAAOQ");

            /*
            var page1 = await page.RunAndWaitForPopupAsync(async () =>
            {
                await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { NameString = "Yes" }).ClickAsync();
            });
            */
            await page.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { NameString = "New" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { Name = "Code, sorted in Ascending order ITEM", Exact = true }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            //await page.FrameLocator("iframe").GetByLabel("#bpoee[role='textbox']").ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Item, Show more" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Description", Exact = true }).ClickAsync();

            string defaultDescription = "Automation Test Item by ML - Test run No. ";
            string currentDateTime = DateTime.Now.ToString();
            int totalNoOfTests = typeof(Tests).GetMethods().Count(m => m.ReturnType == typeof(Task));
            for (int i = 1; i <= totalNoOfTests; i++)
            {
                string testNumber = "Test " + i + "-";
                string fullDescription = defaultDescription + testNumber + currentDateTime;
                await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Description", Exact = true }).FillAsync(fullDescription);
            }
            await page.FrameLocator("iframe").GetByRole(AriaRole.Combobox, new() { NameString = "Item Category Code" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { NameString = "Code N" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Costs & Posting\" / \"" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Unit Cost" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Unit Cost" }).FillAsync("50");
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Prices & Sales\" / \"" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Unit Price" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Unit Price" }).FillAsync("100");
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "RRP" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "RRP" }).FillAsync("110");
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { NameString = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Checkbox, new() { NameString = "Sales Channel 1" }).ClickAsync();
            await page.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Open the page in read-only mode." }).ClickAsync();

            await page.GotoAsync(url: "https://businesscentral.dynamics.com/41e8b83d-2266-4629-b144-97c9e432b787/SANDBOX-AZZTEST");
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screen3.jpg" });
        }

        [Test]
        public async Task Test2()
        {
            using var playwright = await Playwright.CreateAsync();
            // Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, ExecutablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe"}); // Task<IBrowser>
            //Page
            var page = await browser.NewPageAsync();
           
            await page.GotoAsync(url: "https://admin.shopify.com/store/uattaskersolts");
            await page.Locator("[id='account_email']").ClickAsync();
            await page.Locator("[id='account_email']").FillAsync("FILL THE EMAIL ADDRESS IN HERE");
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("email");
            await Task.Delay(TimeSpan.FromSeconds(1));
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("FILL THE PASSWORD IN HERE");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();
           
            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("FILL THE AUTHORISATION CODE IN HERE");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(4));
            await page.GetByRole(AriaRole.Link, new() { Name = "Customers" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add customer" }).ClickAsync();
           
            ShopifyCustomerData shopifyNewCustomer = new ShopifyCustomerData(page);
            var firstRandomName = await shopifyNewCustomer.RandomFirstNameCreation();
            await shopifyNewCustomer.inputCustomerFirstNameData(firstRandomName);

            var secondRandomSurname = await shopifyNewCustomer.RandomSecondNameCreation();
            await shopifyNewCustomer.inputCustomerSecondNameData(secondRandomSurname);

            var customerEmailAddress = firstRandomName + secondRandomSurname + "@gmail.com";
            await shopifyNewCustomer.inputCustomerEmailData(customerEmailAddress);

            var customerPhoneNo = await shopifyNewCustomer.RandomPhoneNumber();
            await shopifyNewCustomer.inputPhoneNumber(customerPhoneNo);

            await shopifyNewCustomer.inputNewCountry();

            var customerCompany = await shopifyNewCustomer.RandomCompanyCreation();
            await shopifyNewCustomer.inputCompany(customerCompany);

            await Task.Delay(TimeSpan.FromSeconds(1));
            
            var customerAddress = await shopifyNewCustomer.RandomAddressCreation();
            await shopifyNewCustomer.inputAddress(customerAddress);

            await Task.Delay(TimeSpan.FromSeconds(1));
            
            var apartmentAddress = await shopifyNewCustomer.RandomApartmentCreation();
            await shopifyNewCustomer.inputAddress(apartmentAddress);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var cityTitle = await shopifyNewCustomer.RandomCityCreation();
            await shopifyNewCustomer.inputCityTitle(cityTitle);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var postalCode = await shopifyNewCustomer.RandomPostalCodeCreation();
            await shopifyNewCustomer.inputPostalCode(postalCode);

            await shopifyNewCustomer.saveButtonMethod();










            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screen7.jpg" });

            await Task.Delay(TimeSpan.FromSeconds(10));

        }
    }
}