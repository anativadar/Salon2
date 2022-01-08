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
    public partial class ServicePage : ContentPage
    {
        Appointment app;
        public ServicePage(Appointment a)
        {
            InitializeComponent();
            app = a;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var service = (Service)BindingContext;
            await App.Database.SaveServiceAsync(service);
            listView.ItemsSource = await App.Database.GetServicesAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var service = (Service)BindingContext;
            await App.Database.DeleteServiceAsync(service);
            listView.ItemsSource = await App.Database.GetServicesAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetServicesAsync();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Service s;
            if (e.SelectedItem != null)
            {
                s = e.SelectedItem as Service;
                var ls = new ListService()
                {
                    AppointmentID = app.ID,
                    ServiceID = s.ID
                };
                await App.Database.SaveListServiceAsync(ls);
                s.ListServices = new List<ListService> { ls };

                await Navigation.PopAsync();
            }
        }
    }
}