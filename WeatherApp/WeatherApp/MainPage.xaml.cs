using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private const string defaultPlaceName = "Apeldoorn", loadingText = "Loading...";
        private ScrollView dataScrollView;
        private Label placeNameLabel;
        private SearchBar searchBar;

        public MainPage()
        {
            var mainGrid = new Grid();

            Content = mainGrid;
            Title = "Weather App";
            Icon = "Images.WeatherIcon.png";
            

            placeNameLabel = new Label()
            {
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center
            };

            dataScrollView = new ScrollView()
            {
                Padding = new Thickness(5, 0)
            };

            searchBar = new SearchBar()
            {
                Text = defaultPlaceName,
                Placeholder = "Place name",
                
            };

            mainGrid.Children.Add(searchBar, 0, 0);
            mainGrid.Children.Add(placeNameLabel, 0, 1);
            mainGrid.Children.Add(dataScrollView, 0, 2);

            Grid.SetColumnSpan(searchBar, 2);
            Grid.SetColumnSpan(placeNameLabel, 2);
            Grid.SetRowSpan(dataScrollView, 4);
            Grid.SetColumnSpan(dataScrollView, 2);

            NavigationPage.SetHasNavigationBar(this, false);

            SetDataForScrollView();

            searchBar.SearchButtonPressed += OnButtonClick;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            SetDataForScrollView(searchBar.Text);
        }

        private async void SetDataForScrollView(string placeName = null)
        {
            SetLoadingText();
            //create grid inside dataScrollView removing previous content
            Grid innerGrid = new Grid();
            dataScrollView.Content = innerGrid;

            //get weather data
            List<WeatherModel> weatherData = await WeatherLogic.GetWeatherDataFor(placeName ?? defaultPlaceName);

            //set place name label
            placeNameLabel.Text = placeName ?? defaultPlaceName;

            if (weatherData.Count == 0)
            {
                innerGrid.Children.Add(new Label() { Text = $"No data for: {placeName ?? defaultPlaceName}." });
            }
            else
            {
                SetDataGridHeaders(innerGrid);

                for (int curr = 0; curr < weatherData.Count; curr++)
                {
                    innerGrid.Children.Add(new Label() { Text = weatherData[curr].WeatherDate.ToString("HH:mm") }, 0, (curr + 1));
                    innerGrid.Children.Add(new Label() { Text = $"{weatherData[curr].Temperature.ToString("0.0")}°" }, 1, (curr + 1));
                    innerGrid.Children.Add(new Label() { Text = $"{weatherData[curr].WeatherDescription}" }, 2, (curr + 1));
                    innerGrid.Children.Add(new Image() { Source = $"http://openweathermap.org/img/w/{weatherData[curr].WeatherIconName}.png" }, 3, (curr + 1));
                }
            }
        }

        private void SetDataGridHeaders(Grid grid)
        {
            grid.Children.Add(new Label() { Text = "Time", FontAttributes = FontAttributes.Bold }, 0, 0);
            grid.Children.Add(new Label() { Text = "Temp", FontAttributes = FontAttributes.Bold }, 1, 0);
            grid.Children.Add(new Label() { Text = "Weather", FontAttributes = FontAttributes.Bold }, 2, 0);
        }

        private void SetLoadingText()
        {
            placeNameLabel.Text = loadingText;
        }
    }
}
