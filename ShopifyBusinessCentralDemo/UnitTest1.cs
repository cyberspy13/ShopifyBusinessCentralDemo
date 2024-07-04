using Microsoft.Playwright;
using ShopifyBusinessCentralDemo.LoginCredentials;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace ShopifyBusinessCentralDemo
{
    public class Tests
    {
        private bool shopDiscount;

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

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("255287");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Stores List" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50153191");
            //await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/products?query=50153191");
            await page.GetByRole(AriaRole.Link, new() { Name = "ZAN FELIX TABLE+ 6 ELISE CHAIRS GREY" }).ClickAsync();
            await page.GetByLabel("Price").ClickAsync();
            string itemPriceValue = await page.GetByLabel("Price").InputValueAsync();
            Console.WriteLine("Item price: " + itemPriceValue);

            await page.GetByRole(AriaRole.Link, new() { Name = "Customers" }).ClickAsync();
            await page.GetByPlaceholder("Search customers").ClickAsync();
            await page.GetByPlaceholder("Search customers").FillAsync("Ben Evans");
            await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/customers?search=Ben%20Evans");
            await page.GetByRole(AriaRole.Link, new() { Name = "Ben Evans" }).ClickAsync();
            await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/customers/7613100753060");
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            //await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/draft_orders/new?customerId=7613100753060");
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
            await page.GetByPlaceholder("0.00").ClickAsync();
            string shippingValue = await page.GetByPlaceholder("0.00").InputValueAsync();
            Console.WriteLine("Shipping value: " + shippingValue);

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
            salesOrderNo = salesOrderNo.Split('?')[0];

            Console.WriteLine("Sales Order No. in Shopify: " + salesOrderNo);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).ClickAsync();
            string lineAmountPrice = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            Console.WriteLine("Item Price in Business Central: " + lineAmountPrice);

            string lineShippingAmount = "10"; // this is a manual field. I was unable to fetch data from the BC side
            Console.WriteLine("Item shipping in Business Central " + lineShippingAmount);



            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();
            Console.WriteLine("Sales order no in Business Central: " + ecommerceOrderRef);

            // validation

            if (ecommerceOrderRef == salesOrderNo)
            {
                Console.WriteLine("Sales order on Shopify and BC side are matched each other");
            }
            else
            {
                Console.WriteLine("Something went wrong.Sales order number on Shopify does not " +
                                  "match same value in Business central");
            }

            if (itemPriceValue == lineAmountPrice & shippingValue == lineShippingAmount)
            {
                Console.WriteLine("Item price in Shopify matched item values in Business Central");
            }
            else { Console.WriteLine("Something went wrong.The items value does not match."); }


        }
        [Test]
        public async Task Test4()
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
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("055372");
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

            await Task.Delay(3000);

            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
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
            await page.GetByPlaceholder("0.00").FillAsync("100");
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            //await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            //await page.WaitForURLAsync("https://admin.shopify.com/store/uattaskersolt/draft_orders/1302404038820");

            await Task.Delay(3000);

            string orderUrl = page.Url;
            int lastSlashIndex = orderUrl.LastIndexOf('/') + 1;
            string salesOrderNo = orderUrl.Substring(lastSlashIndex);
            //salesOrderNo = salesOrderNo.Split('?')[0];
            Console.WriteLine("Sales Order No. in Shopify: " + salesOrderNo);

            await Task.Delay(5000);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();
            Console.WriteLine("Sales order no in Business Central: " + ecommerceOrderRef);

            // validation

            if (ecommerceOrderRef != salesOrderNo)
            {
                Console.WriteLine("Sales order number on Shopify does not " +
                                  "match same value in Business central.No Sales Order related in BC.Test passed");
            }
            else
            {
                Console.WriteLine("Something went wrong.Sales Order from Shopify has been populated in BC.");
            }
        }
        [Test]
        public async Task Test5()
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
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("728681");
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

            await Task.Delay(3000);

            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
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
            await page.GetByPlaceholder("0.00").FillAsync("100");
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByLabel("Edit notes").ClickAsync();
            await page.GetByLabel("Notes", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Notes", new() { Exact = true }).FillAsync("Automation Test by ML");
            string shopifyNotes = await page.GetByLabel("Notes", new() { Exact = true }).InputValueAsync();
            Console.WriteLine(shopifyNotes);
            await page.GetByRole(AriaRole.Button, new() { Name = "Done", Exact = true }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(3000);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Work Description" }).ClickAsync();
            string businessCentralNotes = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Work Description" }).InputValueAsync();

            // validation

            if (shopifyNotes != businessCentralNotes)
            {
                Console.WriteLine("Test failed.The note text does not go  throught to Business Central");
            }
            else
            {
                Console.WriteLine("Test passed.The note text does flow throught to Business Central");
            }
        }
        [Test]
        public async Task Test6()
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
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);

            // await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("227342");
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

            await Task.Delay(3000);

            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(3000);

            string shopifyReferenceNo = await page.EvaluateAsync<string>(@"() => {let elements = Array.from(document.querySelectorAll('span'));
                                        let regex = /#\d{4}/; // matches # followed by any four digits

                                        for (let span of elements) 
                                          {
                                                 if (regex.test(span.textContent)) {
                                                 span.click();
                                                 return span.textContent; // returns the matching text
                                            }
                                          }
    
                return null;}"); // returns null if no matching element was found

            await Task.Delay(10000);

            Console.WriteLine("Shopify reference no. is: " + shopifyReferenceNo);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();


            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "External Document No." }).ClickAsync();
            string businessCentralRefNo = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "External Document No." }).InputValueAsync();
            string shopifySymbol = "#";
            string finalStringEdn = shopifySymbol + businessCentralRefNo;
            Console.WriteLine($"Business Central Reference no. is: " + finalStringEdn);


            // validation

            if (shopifyReferenceNo != finalStringEdn)
            {
                Console.WriteLine("Test failed.The note text does not go  throught to Business Central");
            }
            else
            {
                Console.WriteLine("Test passed.The note text does flow throught to Business Central");
            }
        }

        [Test]
        public async Task Test7()
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
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);

            // await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("767447");
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

            await Task.Delay(3000);

            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Add discount" }).ClickAsync();
            await Task.Delay(1000);
            await page.GetByPlaceholder("Type an order or product discount code").ClickAsync();
            await Task.Delay(1000);
            //await page.GetByText("EXDISPLAY2020% off entire order").ClickAsync();
            await page.GetByText("25% off entire order").ClickAsync();
            string shopDiscountText = await page.GetByText("25% off entire order").InnerTextAsync();
            var regex = new Regex(@"(\d+)"); //regex pattern for extracting numbers
            Match match = regex.Match(shopDiscountText);
            string shopDiscount;
            if (match.Success)
            {
                shopDiscount = match.Value;
                Console.WriteLine(shopDiscount); //prints 20

            }
            else
            {
                shopDiscount = "value not found";
            }

            Console.WriteLine("Shopify discount is: " + shopDiscount);

            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(3000);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Invoice Discount %" }).ClickAsync();
            string businessCentralDiscount = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Invoice Discount %" }).InputValueAsync();
            Console.WriteLine(businessCentralDiscount);
            // validation

            if (shopDiscount != businessCentralDiscount)
            {
                Console.WriteLine("Test failed.The discount value does not go throught to Business Central");
            }
            else
            {
                Console.WriteLine("Test passed.The discount value does flow throught to Business Central");
            }
        }


        [Test]
        public async Task Test8()
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
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);

            // await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();

            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds
            await page.Locator("[id='account_tfa_code']").FillAsync("864593");
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

            await Task.Delay(3000);

            await shopifyNewCustomer.saveButtonMethod();

            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Edit discount" }).ClickAsync();
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("100");
            string discountValue = await page.GetByPlaceholder("0.00").InputValueAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();

            float realDiscountValue = 0;
            if (string.IsNullOrWhiteSpace(discountValue))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {

                realDiscountValue = float.Parse(discountValue);

                // continue with your code
            }
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(3000);
            await page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Link, new() { Name = "ZAN FELIX TABLE+ 6 ELISE CHAIRS GREY" }).ClickAsync();
            await page.GetByLabel("Price").ClickAsync();
            string itemPriceValue = await page.GetByLabel("Price").InputValueAsync();
            float itemPrice = float.Parse(itemPriceValue);

            float itemPriceAfterAppliedDiscount = itemPrice - realDiscountValue;
            Console.WriteLine("Item price after discount is: " + itemPriceAfterAppliedDiscount);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).ClickAsync();
            string lineAmountPrice = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountIntValue = float.Parse(lineAmountPrice);
            Console.WriteLine("Item Price in Business Central: " + lineAmountIntValue);

            // validation

            if (itemPriceAfterAppliedDiscount != lineAmountIntValue)
            {
                Console.WriteLine("Test failed.The discount value does not go throught to Business Central");
            }
            else
            {
                Console.WriteLine("Test passed.The discount value does flow throught to Business Central");
            }
        }

        [Test]
        public async Task Test9()
        {
            using var playwright = await Playwright.CreateAsync();
            // Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, ExecutablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe" }); // Task<IBrowser>                                                                                                                        //Page
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url: "https://admin.shopify.com/store/uattaskersolts");
            await page.Locator("[id='account_email']").ClickAsync();
            await page.Locator("[id='account_email']").FillAsync("info@azzure-creative.com");
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("email");
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(2000);
            // await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds

            await page.Locator("[id='account_tfa_code']").FillAsync("317138");

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
            await Task.Delay(3000);
            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            //await page.Locator(".Polaris-TextField__Segment").First.ClickAsync();
            //await page.Locator(".Polaris-TextField__Segment").First.ClickAsync();
            //await page.Locator(".Polaris-TextField__Segment").First.ClickAsync();
            //await page.Locator(".Polaris-TextField__Segment").First.ClickAsync();
            //await page.Locator(".Polaris-TextField__Segment").First.ClickAsync();
            await page.GetByLabel("available quantity").FillAsync("6");
            await page.GetByLabel("available quantity").ClickAsync();
            string qtyOnShopify = await page.GetByLabel("available quantity").InputValueAsync();
            Console.WriteLine("Quantity on the Shopify side: " + qtyOnShopify);
            await page.GetByRole(AriaRole.Button, new() { Name = "Add shipping or delivery" }).ClickAsync();
            await page.Locator("label:has-text(\"Custom\") span").Nth(2).ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").FillAsync("Shipping charge");
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("200");
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(2000);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Qty. to Assemble to Order", Exact = true }).ClickAsync();
            string qtyOnBusinessCentral = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Qty. to Assemble to Order", Exact = true }).InputValueAsync();

            Console.WriteLine("Item quantity on Business Central: " + qtyOnBusinessCentral);

            // validation

            if (qtyOnShopify != qtyOnBusinessCentral)
            {
                Console.WriteLine("value not found");
            }
            else
            {
                Console.WriteLine("Values are matched. Great job");
            }
        }
        [Test]
        public async Task Test10()
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
            await Task.Delay(1000);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(1000);

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds

            await page.Locator("[id='account_tfa_code']").FillAsync("142507");

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

            await Task.Delay(3000);
            await shopifyNewCustomer.saveButtonMethod();
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();

            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string firstShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();

            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50080308");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string secondShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("ADDIS AIRER GATE FOLD 6MADDIS AIRER GATE FOLD 6MADDIS AIRER GATE FOLD 6M").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();

            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50061493");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string thirdShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("Eveready 60W BC Fireglow BulbEveready 60W BC Fireglow BulbEveready 60W BC Firegl").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();

            await Task.Delay(3000);

            await page.GetByRole(AriaRole.Button, new() { Name = "Add shipping or delivery" }).ClickAsync();
            await page.Locator("label:has-text(\"Custom\") span").Nth(2).ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").FillAsync("Shipping charge");
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("250");
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(5000);
            string orderUrl = page.Url;
            await Task.Delay(2000);
            int lastSlashIndex = orderUrl.LastIndexOf('/') + 1;
            string salesOrderNo = orderUrl.Substring(lastSlashIndex);
            salesOrderNo = salesOrderNo.Split('?')[0];
            Console.WriteLine("Sales Order No. in Shopify: " + salesOrderNo);
            await Task.Delay(5000);

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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();


            //await newPage.Locator("[id = b4tjee]").FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50153191" }).ClickAsync();
            //string firstBusinessCentralItem = await newPage.Locator("[id = b4tjee]").FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50153191" }).InputValueAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50153191" }).ClickAsync();
            string firstBusinessCentralItem = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50153191" }).InnerTextAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50080308" }).ClickAsync();
            string secondBusinessCentralItem = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50080308" }).InnerTextAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50061493" }).ClickAsync();
            string thirdBusinessCentralItem = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "No. 50061493" }).InnerTextAsync();

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();
            Console.WriteLine("Sales order no in Business Central: " + ecommerceOrderRef);


            //Validation
            if (ecommerceOrderRef == salesOrderNo)
            {
                Console.WriteLine("Sales order on Shopify and BC side are matched each other");
            }
            else
            {
                Console.WriteLine("Something went wrong.Sales order number on Shopify does not " +
                                  "match same value in Business central");
            }

            if (firstShopifyItem == firstBusinessCentralItem && secondShopifyItem == secondBusinessCentralItem && thirdShopifyItem == thirdBusinessCentralItem)
            {
                Console.WriteLine("Items SKU numbers are matched and populated in BC fine");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }
        [Test]
        public async Task Test11()
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
            await Task.Delay(1000);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(1000);

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds

            await page.Locator("[id='account_tfa_code']").FillAsync("742053");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Stores List" }).ClickAsync();

            //Getting the Item Price without discounts/shipping costs
            await page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Link, new() { Name = "ZAN FELIX TABLE+ 6 ELISE CHAIRS GREY" }).ClickAsync();
            await page.GetByLabel("Price").ClickAsync();
            string itemPriceValue = await page.GetByLabel("Price").InputValueAsync();
            float itemPrice = float.Parse(itemPriceValue);
            //Creating the customer
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

            await Task.Delay(3000);
            await shopifyNewCustomer.saveButtonMethod();
            //Creating Sales order
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string firstShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            //Adding shipping cost
            await page.GetByRole(AriaRole.Button, new() { Name = "Add shipping or delivery" }).ClickAsync();
            await page.Locator("label:has-text(\"Custom\") span").Nth(2).ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").FillAsync("Shipping charge");
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("250");
            string shippingCost = await page.GetByPlaceholder("0.00").InputValueAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            float realShippingCost = 0;
            if (string.IsNullOrWhiteSpace(shippingCost))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realShippingCost = float.Parse(shippingCost);
            }
            Console.WriteLine("Shipping cost in Shopify: " + realShippingCost);
            //Getting discount in Shopify
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Add discount" }).ClickAsync();
            await Task.Delay(1000);
            await page.GetByLabel("Add custom order discount").CheckAsync();
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("199.99");
            await page.GetByLabel("Reason for discount").ClickAsync();
            await page.GetByPlaceholder("0.00").ClickAsync();
            string discountPrice = await page.GetByPlaceholder("0.00").InputValueAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            float realDiscountPriceInShopify = 0;
            if (string.IsNullOrWhiteSpace(discountPrice))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realDiscountPriceInShopify = float.Parse(discountPrice);
            }
            Console.WriteLine("Item Discount in Shopify is: " + realDiscountPriceInShopify);
            float totalPriceInShopify = itemPrice + realShippingCost;
            float finalTotalPriceInShopify = totalPriceInShopify - realDiscountPriceInShopify;
            await Task.Delay(2000);
            Console.WriteLine("The total item price in Shopify is: " + finalTotalPriceInShopify);
            //Finishing with Sales Order in SHopify
            await Task.Delay(4000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(5000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(5000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(8000);
            //getting the URL customerID
            string orderUrl = page.Url;
            await Task.Delay(2000);
            int lastSlashIndex = orderUrl.LastIndexOf('/') + 1;
            string salesOrderNo = orderUrl.Substring(lastSlashIndex);
            salesOrderNo = salesOrderNo.Split('?')[0];
            Console.WriteLine("Sales Order No. in Shopify: " + salesOrderNo);
            await Task.Delay(5000);

            //create new page in Business Central
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
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Inv. Discount Amount Incl. VAT (GBP)" }).ClickAsync();
            string invoiceDiscountAmountInBc = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Inv. Discount Amount Incl. VAT (GBP)" }).InputValueAsync(); float realInvoiceDiscountAmountInBc = 0;
            float realInvoiceDiscountAmountInBc2 = 0;
            if (string.IsNullOrWhiteSpace(invoiceDiscountAmountInBc))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realInvoiceDiscountAmountInBc2 = float.Parse(invoiceDiscountAmountInBc);
            }
            Console.WriteLine("Discount in Business Central is: " + realInvoiceDiscountAmountInBc2);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).ClickAsync();
            string lineAmountPrice = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountIntValue = float.Parse(lineAmountPrice);
            Console.WriteLine("The total item Price in Business Central: " + lineAmountIntValue);

            float totalSumOfBcAmountAndShippingCost = realShippingCost + lineAmountIntValue;
            float totalItemPriceInBc = totalSumOfBcAmountAndShippingCost - realDiscountPriceInShopify;
            Console.WriteLine("Total Sales order value is: " + totalItemPriceInBc);

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();
            Console.WriteLine("Sales order no in Business Central: " + ecommerceOrderRef);
            await Task.Delay(10000);
            // validation
            if (ecommerceOrderRef == salesOrderNo)
            {
                Console.WriteLine("Sales order on Shopify and BC side are matched each other");
            }
            else
            {
                Console.WriteLine("Something went wrong.Sales order number on Shopify does not " +
                                  "match same value in Business central");
            }
            if (realInvoiceDiscountAmountInBc2 != realDiscountPriceInShopify && totalPriceInShopify != totalItemPriceInBc)
            {
                Console.WriteLine("Test failed.The discount value does not go throught to Business Central");
            }
            else
            {
                Console.WriteLine("Test passed.The discount value and the final price does flow throught to Business Central");
            }
        }
        [Test]
        public async Task Test12()
        {



            using var playwright = await Playwright.CreateAsync();
            // Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, ExecutablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe" }); // Task<IBrowser>
            //Page

            
            
            var page = await browser.NewPageAsync();
            //Login into the page
            await page.GotoAsync(url: "https://admin.shopify.com/store/uattaskersolts");
            await page.Locator("[id='account_email']").ClickAsync();
            await page.Locator("[id='account_email']").FillAsync("info@azzure-creative.com");
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("email");
            await Task.Delay(1000);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();
            await Task.Delay(1000);

            //await page.Locator("[id='h-captcha']").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Continue with email" }).ClickAsync();

            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("@zzC3910!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Use the authentication app" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2)); // wait for 2 seconds

            await page.Locator("[id='account_tfa_code']").FillAsync("773364");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Stores List" }).ClickAsync();
            //Getting the Item Price without discounts/shipping costs
            await page.GetByRole(AriaRole.Link, new() { Name = "Products", Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Link, new() { Name = "ZAN FELIX TABLE+ 6 ELISE CHAIRS GREY" }).ClickAsync();
            await page.GetByLabel("Price").ClickAsync();
            string firstItemPriceValue = await page.GetByLabel("Price").InputValueAsync();                         //1,799.00
            float firstItemPrice = float.Parse(firstItemPriceValue);
            Console.WriteLine("The first item price in Shopify: " + firstItemPrice);

            await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Products", Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50080308");
            await page.GetByRole(AriaRole.Link, new() { Name = "ADDIS AIRER GATE FOLD 6M" }).ClickAsync();
            await page.GetByLabel("Price").ClickAsync();
            string secondItemPriceValue = await page.GetByLabel("Price").InputValueAsync();                        //10.00
            float secondItemPrice = float.Parse(secondItemPriceValue);
            Console.WriteLine("The second item price in Shopify: " + secondItemPrice);

            await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Products", Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search and filter products" }).ClickAsync();
            await page.GetByPlaceholder("Searching all products").ClickAsync();
            await page.GetByPlaceholder("Searching all products").FillAsync("50061493");
            await page.GetByRole(AriaRole.Link, new() { Name = "Eveready 60W BC Fireglow Bulb" }).ClickAsync();
            await page.GetByLabel("Price", new() { Exact = true }).ClickAsync();
            string thirdItemPriceValue = await page.GetByLabel("Price", new() { Exact = true }).InputValueAsync();   //0.99
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Price £" }).ClickAsync();
            float thirdItemPrice = float.Parse(thirdItemPriceValue);
            Console.WriteLine("THe third item price in Shopify: " + thirdItemPrice);

            //Create a new Customer
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

            await Task.Delay(3000);
            await shopifyNewCustomer.saveButtonMethod();
            //Creating Sales Order
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50153191");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string firstShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("ZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELIX TABLE+ 6 ELISE CHAIRS GREYZAN FELI").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();

            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50080308");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string secondShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("ADDIS AIRER GATE FOLD 6MADDIS AIRER GATE FOLD 6MADDIS AIRER GATE FOLD 6M").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();

            await page.GetByPlaceholder("Search products").ClickAsync();
            await page.GetByPlaceholder("Search products").FillAsync("50061493");
            await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").ClickAsync();
            string thirdShopifyItem = await page.GetByRole(AriaRole.Dialog, new() { Name = "Back to browse All products" }).GetByPlaceholder("Search products").InputValueAsync();
            await page.GetByText("Eveready 60W BC Fireglow BulbEveready 60W BC Fireglow BulbEveready 60W BC Firegl").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
            await Task.Delay(3000);
            //Adding shipping cost
            await page.GetByRole(AriaRole.Button, new() { Name = "Add shipping or delivery" }).ClickAsync();
            await page.Locator("label:has-text(\"Custom\") span").Nth(2).ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").ClickAsync();
            await page.GetByPlaceholder("E.g. Free shipping").FillAsync("Shipping charge");
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("1000");
            string shippingCost = await page.GetByPlaceholder("0.00").InputValueAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            float realShippingCost = 0;                                                         //shipment cost 
            if (string.IsNullOrWhiteSpace(shippingCost))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realShippingCost = float.Parse(shippingCost);
            }
            Console.WriteLine("Shipping cost in Shopify: " + realShippingCost);
            //Getting discount in Shopify
            await Task.Delay(2000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Add discount" }).ClickAsync();
            await Task.Delay(1000);
            await page.GetByLabel("Add custom order discount").CheckAsync();
            await page.GetByPlaceholder("0.00").ClickAsync();
            await page.GetByPlaceholder("0.00").FillAsync("99.99");
            await page.GetByLabel("Reason for discount").ClickAsync();
            await page.GetByPlaceholder("0.00").ClickAsync();
            string discountPrice = await page.GetByPlaceholder("0.00").InputValueAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
            float realDiscountPriceInShopify = 0;
            if (string.IsNullOrWhiteSpace(discountPrice))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realDiscountPriceInShopify = float.Parse(discountPrice);
            }
            Console.WriteLine("Item Discount in Shopify is: " + realDiscountPriceInShopify);
            float totalPriceInShopify = firstItemPrice + secondItemPrice + thirdItemPrice + realShippingCost;
            float finalTotalPriceInShopify = totalPriceInShopify - realDiscountPriceInShopify;
            await Task.Delay(2000);
            Console.WriteLine("The total item price in Shopify is: " + finalTotalPriceInShopify);
            //Finishing with Sales Order in SHopify
            await Task.Delay(4000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Collect payment" }).ClickAsync();
            await Task.Delay(5000);
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Mark as paid" }).ClickAsync();
            await Task.Delay(5000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Create order" }).ClickAsync();
            await Task.Delay(8000);
            //getting the URL customerID
            string orderUrl = page.Url;
            await Task.Delay(2000);
            int lastSlashIndex = orderUrl.LastIndexOf('/') + 1;
            string salesOrderNo = orderUrl.Substring(lastSlashIndex);
            salesOrderNo = salesOrderNo.Split('?')[0];
            Console.WriteLine("Sales Order No. in Shopify: " + salesOrderNo);
            await Task.Delay(5000);

           

            //create new page in Business Central
            var context = await browser.NewContextAsync();
            // Create a new page in this context
            var newPage = await context.NewPageAsync();
            // Login into Business central
            await newPage.GotoAsync(url: "https://businesscentral.dynamics.com/41e8b83d-2266-4629-b144-97c9e432b787/SANDBOX-AZZTEST");

            string myUsername = "mihail.lecari@azzure.onmicrosoft.com";
            string myPassword = "qu!ckHat70";

            await newPage.Locator("[name='loginfmt']").FillAsync(myUsername);
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
            await newPage.GetByPlaceholder("Password").ClickAsync();
            await newPage.Locator("[name='passwd']").FillAsync(myPassword);
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Yes" }).ClickAsync();
            //Search for the latest Sales Order
            await newPage.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Tell me what you want to do" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Tell me what you want to do" }).FillAsync("Sales Orders");
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Gridcell, new() { Name = "\" / \" Sales Orders Lists Not bookmarked" }).GetByText("Sales Orders").ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Columnheader, new() { Name = "No., sorted in Ascending order Ascending Open menu for No." }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByText("Descending").ClickAsync();
            await Task.Delay(2000);
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Manage" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitem, new() { Name = "Edit" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "General, Show more" }).ClickAsync();
            //await newPage.FrameLocator("iframe").GetByRole(AriaRole.Menuitemcheckbox, new() { Name = "Toggle FactBox" }).ClickAsync();
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Inv. Discount Amount Incl. VAT (GBP)" }).ClickAsync();
            string invoiceDiscountAmountInBc = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Inv. Discount Amount Incl. VAT (GBP)" }).InputValueAsync();
            float realInvoiceDiscountAmountInBc = 0;
            if (string.IsNullOrWhiteSpace(invoiceDiscountAmountInBc))
            {
                Console.WriteLine("Handle empty value case");
            }
            else
            {
                realInvoiceDiscountAmountInBc = float.Parse(invoiceDiscountAmountInBc);
            }
            Console.WriteLine("Item discount in Business Central is: " + realInvoiceDiscountAmountInBc);
            newPage.WaitForLoadStateAsync();
            /*
            //This have to be looked in the future because the way it is working linked to the IFrame which has additional security level
            var businessCentralTable = await newPage.QuerySelectorAllAsync(".ms-nav-grid-data-table tr td.edit-container:nth-child(4)");

            foreach (var businessCentralvalue in businessCentralTable)
            { 
                string dataText = await businessCentralvalue.InnerTextAsync();
                Console.WriteLine(dataText);
            }
            */
            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).ClickAsync();
            string firstItemPriceInBc = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountIntValueForTheFirstItem = float.Parse(firstItemPriceInBc);
            Console.WriteLine("Item price in BC: " + lineAmountIntValueForTheFirstItem);

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT 10.00", Exact = true }).ClickAsync(new LocatorClickOptions { Timeout = 60000});
            string secondItemPriceInBc = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountIntValueForTheSecondtItem = float.Parse(secondItemPriceInBc);
            Console.WriteLine("Item price in BC: " + lineAmountIntValueForTheSecondtItem);

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT 0.99", Exact = true }).ClickAsync(new LocatorClickOptions { Timeout = 60000 });
            string thirdItemPriceInBc = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountIntValueForTheThirdItem = float.Parse(thirdItemPriceInBc);
            Console.WriteLine("Item price in BC: " + lineAmountIntValueForTheThirdItem);

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT 1,000.00", Exact = true }).ClickAsync(new LocatorClickOptions { Timeout = 60000 });
            string shippingCostInBC = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Line Amount Incl. VAT", Exact = true }).InputValueAsync();
            float lineAmountOfshippingCostInBC = float.Parse(shippingCostInBC);
            Console.WriteLine("Shipping cost in BC: " + lineAmountOfshippingCostInBC);



            // here we need to get the values for the second and third item cost in BC


            float totalSumOfBcAmountAndShippingCost = lineAmountIntValueForTheFirstItem + lineAmountIntValueForTheSecondtItem + lineAmountIntValueForTheThirdItem + lineAmountOfshippingCostInBC;
            float totalItemPriceInBc = totalSumOfBcAmountAndShippingCost - realInvoiceDiscountAmountInBc;
            Console.WriteLine("Total Sales order value is: " + totalItemPriceInBc);

            await newPage.FrameLocator("iframe").GetByRole(AriaRole.Button, new() { Name = "Azzure IT - Ecommerce Details\" / \"" }).ClickAsync();
            string ecommerceOrderRef = await newPage.FrameLocator("iframe").GetByRole(AriaRole.Textbox, new() { Name = "Ecommerce Order Ref" }).InputValueAsync();
            Console.WriteLine("Sales order no in Business Central: " + ecommerceOrderRef);
            await Task.Delay(10000);

            //Validation

            

            if (ecommerceOrderRef == salesOrderNo)
            {
                Console.WriteLine("Test passed.");
            }
            else
            {
                Console.WriteLine("Test failed.");
            }

            if (firstItemPrice == lineAmountIntValueForTheFirstItem && secondItemPrice == lineAmountIntValueForTheSecondtItem && thirdItemPrice == lineAmountIntValueForTheThirdItem)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }


            
            
        






    
        }

    }
}




















    
