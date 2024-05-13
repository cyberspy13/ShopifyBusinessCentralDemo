using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task inputCustomerFirstNameData(string firstName)
        {
            await _firstName.FillAsync(firstName);

        }
        public async Task inputCustomerSecondNameData(string lastName)
        {
            await _lastName.FillAsync(lastName);

        }
        public async Task inputCustomerEmailData(string emailAddress)
        {
            await _emailAddress.FillAsync(emailAddress);

        }
        public async Task inputPhoneNumber(int phoneNumber)
        {
            await _phoneNumber.FillAsync("+4477" + phoneNumber.ToString());

        }
        public async Task inputNewCountry()
        {
            //await _countrySelection.ClickAsync();
            await _page.Locator("[name='customer[country]']").SelectOptionAsync("Ukraine");
        }
        public async Task inputCompany(string company)
        {
            await _company.FillAsync(company);
        }
        public async Task inputAddress(string address)
        {
            await _address.FillAsync(address);

        }
        public async Task inputApartment(string apartment)
        {
            await _address.FillAsync(apartment);

        }
        public async Task inputCityTitle(string city)
        {
            await _city.FillAsync(city);
        }
        public async Task inputPostalCode(string postalCode)
        {
            await _postcode.FillAsync(postalCode);
        }
        public async Task saveButtonMethod()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            
        }
        public async Task<string> RandomFirstNameCreation()
        {

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string firstName = obj.results[0].name.first;
                    //string lastName = obj.results[0].name.last;


                    return $"{firstName}";

                }
            }
            return null;

            //this is old version of the code based on array
            /* 
            Random rand = new Random();
            string[] names = { "John", "Jane", "Robert", "Emma", "Michael", "Emily", "Joseph" };
            string randomFirstName = names[rand.Next(names.Length)];
            return randomFirstName;
            */
        }
        public async Task<string> RandomSecondNameCreation()
        {

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    //string firstName = obj.results[0].name.first;
                    string lastName = obj.results[0].name.last;


                    return $"{lastName+"-Automation-Test-by-ML"}";

                }
            }
            return null;

            //this is old version of the code based on array
            /*
            Random rand = new Random();
            string[] names = { "Smith", "Brookland", "Kiosaki", "Philips", "Howe", "Thatcher", "Stalin" };
            string randomSecondName = names[rand.Next(names.Length)];
            return randomSecondName;
            */
        }
        public async Task<int> RandomPhoneNumber()
        {
            Random rand = new Random();
            return rand.Next(10000000, 99999999);
        }
        public async Task<string> RandomCompanyCreation()
        {
            Random rand = new Random();
            string[] names = { "CISCO", "HILTON", "DHL", "Softcat", "Baringa", "Bristol Myers Squib", "Sopra Steria" };
            string randomCompanyName = names[rand.Next(names.Length)];
            return randomCompanyName;
        }
        public async Task<string> RandomAddressCreation()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string street = obj.results[0].location.street.name;
                    //string city = obj.results[0].location.city;
                    string state = obj.results[0].location.state;

                    return $"{street}, {state}";

                }
            }
            return null;
        }
        public async Task<string> RandomApartmentCreation()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string apartmentNumber = obj.results[0].location.street.number;

                    return $"{apartmentNumber}";

                }
            }
            return null;
        }
        public async Task<string> RandomCityCreation()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string city = obj.results[0].location.city;

                    return $"{city}";
                }
            }
            return null;
        }
        public async Task<string> RandomPostalCodeCreation()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    string postcode = obj.results[0].location.postcode;

                    return $"{postcode}";
                }
            }
            return null;
        }


    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
    
}
