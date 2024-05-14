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
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, ExecutablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe" }); // Task<IBrowser>
            //Page
            var page = await browser.NewPageAsync();

            await page.GotoAsync(url: "https://admin.shopify.com/store/uattaskersolts");
            await page.Locator("[id='account_email']").ClickAsync();
            await page.Locator("[id='account_email']").FillAsync("info@azzure-creative.com");
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("email");
            await Task.Delay(TimeSpan.FromSeconds(1));
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.Locator("[id='h-captcha']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("881895");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Stores List" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Customers" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add customer" }).ClickAsync();

            ShopifyCustomerData shopifyNewCustomer = new ShopifyCustomerData(page);
            var firstRandomName = await shopifyNewCustomer.FetchRandomdataCreation();
            var customerCreationParts = firstRandomName.Split(',');
            await shopifyNewCustomer.InputCustomerData(customerCreationParts[0], customerCreationParts[1],
                                                       customerCreationParts[2], customerCreationParts[3],
                                                       customerCreationParts[4], customerCreationParts[5],
                                                       customerCreationParts[6], customerCreationParts[7],
                                                       customerCreationParts[8]);

            var PhoneNo = await shopifyNewCustomer.RandomPhoneNumber();
            await shopifyNewCustomer.inputPhoneNumber(PhoneNo);

            var customerCompany = await shopifyNewCustomer.RandomCompanyCreation();
            await shopifyNewCustomer.inputCompany(customerCompany);

            await Task.Delay(TimeSpan.FromSeconds(3));

            await shopifyNewCustomer.saveButtonMethod();

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screen10.jpg" });

            await Task.Delay(TimeSpan.FromSeconds(10));

        }
        [Test]
        public async Task Test3()
        {
            using var playwright = await Playwright.CreateAsync();
            // Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, ExecutablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe" }); // Task<IBrowser>
            //Page
            var page = await browser.NewPageAsync();

            await page.GotoAsync(url: "https://admin.shopify.com/store/uattaskersolts");
            await page.Locator("[id='account_email']").ClickAsync();
            await page.Locator("[id='account_email']").FillAsync("info@azzure-creative.com");
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("email");
            await Task.Delay(TimeSpan.FromSeconds(1));
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));

            await page.Locator("[id='h-captcha']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("605059");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Stores List" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Customers" }).ClickAsync();
            await page.GetByPlaceholder("Search customers").ClickAsync();
            await page.GetByPlaceholder("Search customers").FillAsync("Ben Evans");
            await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/customers?search=Ben%20Evans");
            await page.GetByRole(AriaRole.Link, new() { Name = "Ben Evans" }).ClickAsync();
            await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/customers/7613100753060");
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/draft_orders/new?customerId=7613100753060");
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            
            await page.GetByRole(AriaRole.Button, new() { Name = "Add shipping or delivery" }).ClickAsync();
            await page.Locator("label:has-text(\"Custom\") span").Nth(2).ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").FillAsync("Shipping charge");
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("10");
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
           
            await Task.Delay(3000);

            string orderUrl = page.Url;
            int lastSlashIndex = orderUrl.LastIndexOf('/') + 1;
            string salesOrderNo = orderUrl.Substring(lastSlashIndex);

            Console.WriteLine(salesOrderNo);

            await Task.Delay(8000);

            var context = await browser.NewContextAsync();

            // Create a new page in this context
            var newPage = await context.NewPageAsync();

            // Now use 'newPage' for your tests like you used 'page'
            await newPage.GotoAsync(url: "https://businesscentral.dynamics.com/41e8b83d-2266-4629-b144-97c9e432b787/SANDBOX-AZZTEST");

            string myUsername = "mihail.lecari@azzure.onmicrosoft.com";
            string myPassword = "qu!ckHat70";

            await newPage.Locator("[name='loginfmt']").FillAsync(myUsername);
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
            await newPage.GetByPlaceholder("Password").ClickAsync();
            await newPage.Locator("[name='passwd']").FillAsync(myPassword);
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Yes" }).ClickAsync();
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Tell me what you want to do" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Tell me what you want to do" }).FillAsync("Sales Orders");
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { Name = "\" / \" Sales Orders Lists Not bookmarked" }).GetByText("Sales Orders").ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Columnheader, new() { Name = "No., sorted in Ascending order Ascending Open menu for No." }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByText("Descending").ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No., sorted in Descending order SO00047548" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();

            Console.WriteLine(ecommerceOrderRef);

        }
    }
}