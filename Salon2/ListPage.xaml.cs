using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Salon2.Models;

namespace Salon2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var app = (Appointment)BindingContext;
            app.Date = DateTime.UtcNow;
            await App.Database.SaveAppointmentAsync(app);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var app = (Appointment)BindingContext;
            await App.Database.DeleteAppointmentAsync(app);
            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ServicePage((Appointment)
           this.BindingContext)
            {
                BindingContext = new Service()
            });

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var app = (Appointment)BindingContext;

            listView.ItemsSource = await App.Database.GetListServicesAsync(app.ID);
        }
    }
}