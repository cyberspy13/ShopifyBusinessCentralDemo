using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBusinessCentralDemo.LoginCredentials
{
    public class ShopifyCustomerData
    {
        private IPage _page;
        private object rand;
        private readonly ILocator _firstName;
        private readonly ILocator _lastName;
        private readonly ILocator _emailAddress;
        private readonly ILocator _phoneNumber;
        private readonly ILocator _countrySelection;
        private readonly ILocator _pickCountry;
        private readonly ILocator _company;
        private readonly ILocator _address;
        private readonly ILocator _apartment;
        private readonly ILocator _city;
        private readonly ILocator _postcode;

        public ShopifyCustomerData(IPage page)
        {
            _page = page;

            _firstName = _page.Locator("[name='firstName']");
            _lastName = _page.Locator("[name='lastName']");
            _emailAddress = _page.Locator("[name='email']");
            _phoneNumber = _page.Locator("[name='phone']");
            _countrySelection = _page.Locator("[name='customer[country]']");
            _company = _page.Locator("[name='customer[company]']");
            _address = _page.Locator("[name='customer[address1]']");
            _apartment = _page.Locator("[name='customer[address2]']");
            _city = _page.Locator("[name='customer[city]']");
            _postcode = _page.Locator("[name='customer[zip]']");
            
        }
        public async Task InputCustomerData(string firstName,string lastName,string emailAddress,
                                            string addressName, string addressState, string apartmentNo,
                                            string city, string postcode,string country)
        {
            await _firstName.FillAsync(firstName);
            await _lastName.FillAsync(lastName);
            await _emailAddress.FillAsync(emailAddress);
            await _address.FillAsync($"{addressName},{addressState}");
            await _apartment.FillAsync(apartmentNo);
            await _city.FillAsync(city);
            await _postcode.FillAsync(postcode);
            await _countrySelection.SelectOptionAsync(country);
           
            /*
            bool isOptionalAvailable = await _page.Locator($"option[value='{country}']").IsVisibleAsync();
            if (isOptionalAvailable)
            {
                await _page.Locator("[name='customer[country]']").SelectOptionAsync(country);
            }
            else
            {
                await _page.Locator("[name='customer[country]']").SelectOptionAsync("United Kingdom");
            }
            */
        }
        public async Task inputPhoneNumber(int phoneNumber)
        {
            await _phoneNumber.FillAsync("+4477" + phoneNumber.ToString());
        }
       
        public async Task inputCompany(string company)
        {
            await _company.FillAsync(company);
        }
        public async Task saveButtonMethod()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            
        }
        public async Task<string> FetchRandomdataCreation()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string firstName = obj.results[0].name.first;
                    string lastName = obj.results[0].name.last;
                    string emailAddress = obj.results[0].email;
                    string randomAddressName = obj.results[0].location.street.name;
                    string randomAddressState = obj.results[0].location.state;
                    string apartmentNumber = obj.results[0].location.street.number;
                    string city = obj.results[0].location.city;
                    string postcode = obj.results[0].location.postcode;
                    string country = obj.results[0].location.country;

                    return $"{firstName},{lastName},{emailAddress},{randomAddressName}," +
                        $"{randomAddressState},{apartmentNumber},{city},{postcode},{country}";

                }
            }
            return null;
        }
        public async Task<int> RandomPhoneNumber()
        {
            Random rand = new Random();
            return rand.Next(10000000, 99999999);
        }
        public async Task<string> RandomCompanyCreation()
        {
            Random rand = new Random();
            string[] names = { "CISCO", "HILTON", "DHL", "Softcat", "Baringa", "Bristol Myers Squib", "Sopra Steria","Advania UK", "Azzure LTD" };
            string randomCompanyName = names[rand.Next(names.Length)];
            return randomCompanyName;
        }
        
      
    }
    
}
