﻿using Microsoft.Playwright;
using ShopifyBusinessCentralDemo.LoginCredentials;
using System.Diagnostics.CodeAnalysis;
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





    }
}