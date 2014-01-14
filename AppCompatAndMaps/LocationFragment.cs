using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace AppCompatAndMaps
{
    public class LocationFragment : Fragment
    {
        private MapView _mapView;
        private GoogleMap _map;
        private Marker _meMarker;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.location, container, false);
            _mapView = view.FindViewById<MapView>(Resource.Id.mapView);
            _mapView.OnCreate(savedInstanceState);

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            
            MapsInitializer.Initialize(Activity);
        }

        public override void OnStart()
        {
            base.OnStart();
            InitializeMapAndHandlers();
        }

        private void InitializeMapAndHandlers()
        {
            SetUpMapIfNeeded();

            if (_map != null)
            {
                _map.MyLocationEnabled = true;
                _map.UiSettings.CompassEnabled = true;

                if (_meMarker == null)
                    CreateMarker();
            }
        }

        private void CreateMarker()
        {
            var latLng = new LatLng(55.816887, 12.532878);

            var markerOptions = new MarkerOptions()
                .SetPosition(latLng)
                .Draggable(true);
            _meMarker = _map.AddMarker(markerOptions);
            _map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latLng, 13));
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _mapView.OnDestroy();
        }

        public override void OnResume()
        {
            base.OnResume();
            SetUpMapIfNeeded();
            _mapView.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            _mapView.OnLowMemory();
        }

        private void SetUpMapIfNeeded()
        {
            if (null == _map)
            {
                _map = View.FindViewById<MapView>(Resource.Id.mapView).Map;
            }
        }
    }
}