using Countries.Models;
using Countries.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Countries.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CountryName> _countriesList;
        private List<CountryName> _countryNames;
        private CountryName _selectedCountry;
        private bool _isEnabled;
        private bool _isVisible;
        private DelegateCommand _infoCommand;
        private IApiService _apiService;
        private List<Country> _countries { get; set; }
        private bool _byName;
        private bool _bySize;
        private bool _byPopulation;

        private string _altSpellings;
        private double _area;
        private string _borders;
        private string _callingCodes;
        private string _capital;
        private string _currencies;
        private string _demonym;
        private string _flag;
        private string _language;
        private string _latLng;
        private string _region;
        private string _regionalBlocs;
        private string _subRegion;
        private long _population;
        private string _timeZones;
        private string _topLevelDomain;

        public MainPageViewModel(INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            IsEnabled = true;
            _apiService = apiService;
            LoadContries();
        }

        public bool ByName
        {
            get => _byName;
            set => SetProperty(ref _byName, value, OrderName);
        }

        public bool BySize
        {
            get => _bySize;
            set => SetProperty(ref _bySize, value, OrderSize);
        }

        public bool ByPopulation
        {
            get => _byPopulation;
            set => SetProperty(ref _byPopulation, value, OrderPopulation);
        }



        public ObservableCollection<CountryName> CountriesList
        {
            get => _countriesList;
            set => SetProperty(ref _countriesList, value);
        }
        public string AltSpellings
        {
            get => _altSpellings;
            set => SetProperty(ref _altSpellings, value);
        }
        public double Area
        {
            get => _area;
            set => SetProperty(ref _area, value);
        }
        public string Borders
        {
            get => _borders;
            set => SetProperty(ref _borders, value);
        }
        public string CallingCodes
        {
            get => _callingCodes;
            set => SetProperty(ref _callingCodes, value);
        }
        public string Capital
        {
            get => _capital;
            set => SetProperty(ref _capital, value);
        }
        public string Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }
        public string Demonym
        {
            get => _demonym;
            set => SetProperty(ref _demonym, value);
        }
        public string Flag
        {
            get => _flag;
            set => SetProperty(ref _flag, value);
        }
        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }
        public string LatLng
        {
            get => _latLng;
            set => SetProperty(ref _latLng, value);
        }
        public string Region
        {
            get => _region;
            set => SetProperty(ref _region, value);
        }
        public string RegionalBlocs
        {
            get => _regionalBlocs;
            set => SetProperty(ref _regionalBlocs, value);
        }
        public string SubRegion
        {
            get => _subRegion;
            set => SetProperty(ref _subRegion, value);
        }
        public long Population
        {
            get => _population;
            set => SetProperty(ref _population, value);
        }
        public string TimeZones
        {
            get => _timeZones;
            set => SetProperty(ref _timeZones, value);
        }
        public string TopLevelDomain
        {
            get => _topLevelDomain;
            set => SetProperty(ref _topLevelDomain, value);
        }


        public CountryName SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        public DelegateCommand InfoCommand => _infoCommand ?? (_infoCommand = new DelegateCommand(Info));

        private async void Info()
        {
            IsVisible = false;
            if (SelectedCountry == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes seleccionar un pais", "Accept");
                return;
            }

            var country = _countries.Where(c => c.alpha3Code == SelectedCountry.Id).FirstOrDefault();
            if (country == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No encontramos infomracíon del pais", "Accept");
                return;
            }

            var borders = string.Empty;
            foreach (var border in country.borders)
            {
                var borderName = _countries
                    .Where(c => c.alpha3Code == border)
                    .FirstOrDefault();
                borders += string.Format("{0}, ", borderName.name);
            }

            var languages = string.Empty;
            foreach (var language in country.languages)
            {
                languages += string.Format("{0} ({1}), ", language.name, language.nativeName);
            }

            var timeZones = string.Empty;
            foreach (var timeZone in country.timezones)
            {
                timeZones += string.Format("{0}, ", timeZone);
            }

            var currencies = string.Empty;
            foreach (var currency in country.currencies)
            {
                currencies += string.Format("{0} {1}, ", currency.symbol, currency.name);
            }

            var regionalBlocs = string.Empty;
            foreach (var regionalBloc in country.regionalBlocs)
            {
                regionalBlocs += string.Format("{0}, ", regionalBloc.acronym);
            }

            var topLevelDomains = string.Empty;
            foreach (var topLevelDomain in country.topLevelDomain)
            {
                topLevelDomains += string.Format("{0}, ", topLevelDomain);
            }

            var callingCodes = string.Empty;
            foreach (var callingCode in country.callingCodes)
            {
                callingCodes += string.Format("{0}, ", callingCode);
            }

            var altSpellings = string.Empty;
            foreach (var altSpelling in country.altSpellings)
            {
                altSpellings += string.Format("{0}, ", altSpelling);
            }

            var latLon = "N/D";
            if (country.latlng.Count >0)
            {
                 latLon = string.Format("{0}, {1}", country.latlng[0], country.latlng[1]);
            }
            AltSpellings = altSpellings;
            Area = country.area ?? 0;
            Borders = borders;
            CallingCodes = callingCodes;
            Capital = country.capital;
            Currencies = currencies;
            Demonym = country.demonym;
            Flag = country.flag;
            Language = languages;
            LatLng = latLon;
            Population = country.population;
            Region = country.region;
            SubRegion = country.subregion;
            RegionalBlocs = regionalBlocs;
            TopLevelDomain = topLevelDomains;
            TimeZones = timeZones;
            IsVisible = true;
        }

        private async void LoadContries()
        {
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetList<Country>(url, "/rest/v2/all");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se puedo obtener la información", "Accept");
                IsEnabled = true;
                return;
            }
            _countries = (List<Country>)response.Result;
            _countryNames = new List<CountryName>();
            foreach (var country in _countries)
            {
                _countryNames.Add(new CountryName
                {
                    Id = country.alpha3Code,
                    Name = country.translations.es ?? country.name,
                    Population = country.population,
                    Area = country.area
                });
            }
            ByName = true;
            IsEnabled = true;
        }

        private void OrderSize()
        {
            if (BySize)
            {
                ByName = false;
                ByPopulation = false;
                CountriesList = new ObservableCollection<CountryName>(
                    _countryNames.OrderBy(o => o.Area).ToList());
            }
        }
        private void OrderName()
        {
            if (ByName)
            {
                BySize = false;
                ByPopulation = false;

                CountriesList = new ObservableCollection<CountryName>(
                       _countryNames.OrderBy(o => o.Name).ToList());
            }
        }
        private void OrderPopulation()
        {
            if (ByPopulation)
            {
                ByName = false;
                BySize = false;
                CountriesList = new ObservableCollection<CountryName>(
                    _countryNames.OrderBy(o => o.Population).ToList());
            }
        }
    }
}
