﻿// Copyright 2012 - 2016 Andreas Jakl. All rights reserved.
// NDEF Library for Proximity APIs / NFC
// http://andijakl.github.io/ndef-nfc/
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Networking.Proximity;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using NdefLibrary.Ndef;
using NdefLibraryUwp.Ndef;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NdefDemoWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ProximityDevice _device;
        private long _subscriptionIdNdef;
        private long _publishingMessageId;
        private readonly CoreDispatcher _dispatcher;
        private readonly ResourceLoader _loader = new ResourceLoader();


        public MainPage()
        {
            InitializeComponent();

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            // Update enabled / disabled state of buttons in the User Interface
            UpdateUiForNfcStatusAsync();
        }



        private void BtnInitNfc_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            // Initialize NFC
            _device = ProximityDevice.GetDefault();
            // Subscribe for arrived / departed events
            if (_device != null)
            {
                _device.DeviceArrived += NfcDeviceArrived;
                _device.DeviceDeparted += NfcDeviceDeparted;
            }
            // Update status text for UI
            SetStatusOutput(_loader.GetString(_device != null ? "StatusInitialized" : "StatusInitFailed"));
            // Update enabled / disabled state of buttons in the User Interface
            UpdateUiForNfcStatusAsync();
        }

        #region Device Arrived / Departed
        private void NfcDeviceDeparted(ProximityDevice sender)
        {
            SetStatusOutput(_loader.GetString("DeviceDeparted"));
            SetStatusImage(null);
        }

        private void NfcDeviceArrived(ProximityDevice sender)
        {
            SetStatusOutput(_loader.GetString("DeviceArrived"));
        }
        #endregion

        #region Subscribe for tags
        // ----------------------------------------------------------------------------------------------------
        private void BtnSubscribeNdef_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            // Only subscribe for messages if no NDEF subscription is already active
            if (_subscriptionIdNdef != 0) return;
            // Ask the proximity device to inform us about any kind of NDEF message received from
            // another device or tag.
            // Store the subscription ID so that we can cancel it later.
            _subscriptionIdNdef = _device.SubscribeForMessage("NDEF", MessageReceivedHandler);
            // Update status text for UI
            SetStatusOutput(string.Format(_loader.GetString("StatusSubscribed"), _subscriptionIdNdef));
            // Update enabled / disabled state of buttons in the User Interface
            UpdateUiForNfcStatusAsync();
        }

        private async void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
            // Get the raw NDEF message data as byte array
            var rawMsg = message.Data.ToArray();

            NdefMessage ndefMessage;
            try
            {
                // Let the NDEF library parse the NDEF message out of the raw byte array
                ndefMessage = NdefMessage.FromByteArray(rawMsg);
            }
            catch (NdefException e)
            {
                SetStatusOutput(string.Format(_loader.GetString("InvalidNdef"), e.Message));
                return;
            }

            // Analysis result
            var tagContents = new StringBuilder();

            // Parse tag contents
            try
            {
                // Clear bitmap if the last tag contained an image
                SetStatusImage(null);

                // Parse the contents of the tag
                await ParseTagContents(ndefMessage, tagContents);

                // Update status text for UI
                SetStatusOutput(string.Format(_loader.GetString("StatusTagParsed"), tagContents));
            }
            catch (Exception ex)
            {
                SetStatusOutput(string.Format(_loader.GetString("StatusNfcParsingError"), ex.Message));
            }

        }

        private async Task ParseTagContents(NdefMessage ndefMessage, StringBuilder tagContents)
        {
            // Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {
                // --------------------------------------------------------------------------
                // Print generic information about the record
                if (record.Id != null && record.Id.Length > 0)
                {
                    // Record ID (if present)
                    tagContents.AppendFormat("Id: {0}\n", Encoding.UTF8.GetString(record.Id, 0, record.Id.Length));
                }
                // Record type name, as human readable string
                tagContents.AppendFormat("Type name: {0}\n", ConvertTypeNameFormatToString(record.TypeNameFormat));
                // Record type
                if (record.Type != null && record.Type.Length > 0)
                {
                    tagContents.AppendFormat("Record type: {0}\n",
                        Encoding.UTF8.GetString(record.Type, 0, record.Type.Length));
                }

                // --------------------------------------------------------------------------
                // Check the type of each record
                // Using 'true' as parameter for CheckSpecializedType() also checks for sub-types of records,
                // e.g., it will return the SMS record type if a URI record starts with "sms:"
                // If using 'false', a URI record will always be returned as Uri record and its contents won't be further analyzed
                // Currently recognized sub-types are: SMS, Mailto, Tel, Nokia Accessories, NearSpeak, WpSettings
                var specializedType = record.CheckSpecializedType(true);

                if (specializedType == typeof(NdefMailtoRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract Mailto record info
                    var mailtoRecord = new NdefMailtoRecord(record);
                    tagContents.Append("-> Mailto record\n");
                    tagContents.AppendFormat("Address: {0}\n", mailtoRecord.Address);
                    tagContents.AppendFormat("Subject: {0}\n", mailtoRecord.Subject);
                    tagContents.AppendFormat("Body: {0}\n", mailtoRecord.Body);
                }
                else if (specializedType == typeof(NdefUriRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract URI record info
                    var uriRecord = new NdefUriRecord(record);
                    tagContents.Append("-> URI record\n");
                    tagContents.AppendFormat("URI: {0}\n", uriRecord.Uri);
                }
                else if (specializedType == typeof(NdefSpRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract Smart Poster info
                    var spRecord = new NdefSpRecord(record);
                    tagContents.Append("-> Smart Poster record\n");
                    tagContents.AppendFormat("URI: {0}", spRecord.Uri);
                    tagContents.AppendFormat("Titles: {0}", spRecord.TitleCount());
                    if (spRecord.TitleCount() > 1)
                        tagContents.AppendFormat("1. Title: {0}", spRecord.Titles[0].Text);
                    tagContents.AppendFormat("Action set: {0}", spRecord.ActionInUse());
                    // You can also check the action (if in use by the record), 
                    // mime type and size of the linked content.
                }
                else if (specializedType == typeof(NdefVcardRecordBase))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract business card info
                    var vcardRecord = new NdefVcardRecord(record);
                    tagContents.Append("-> Business Card record" + Environment.NewLine);
                    var contact = vcardRecord.ContactData;

                    // Contact has phone or email info set? Use contact manager to show the contact card
                    await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        if (contact.Emails.Any() || contact.Phones.Any())
                        {
                            var rect = GetElementRect(StatusOutput);
                            ContactManager.ShowContactCard(contact, rect, Placement.Below);
                        }
                        else
                        {
                            // No phone or email set - contact manager would not show the contact card.
                            // -> parse manually
                            tagContents.AppendFormat("Name: {0}\n", contact.DisplayName);
                            tagContents.Append("[not parsing other values in the demo app]");
                        }
                    });
                }
                else if (specializedType == typeof(NdefLaunchAppRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract LaunchApp record info
                    var launchAppRecord = new NdefLaunchAppRecord(record);
                    tagContents.Append("-> LaunchApp record" + Environment.NewLine);
                    if (!string.IsNullOrEmpty(launchAppRecord.Arguments))
                        tagContents.AppendFormat("Arguments: {0}\n", launchAppRecord.Arguments);
                    if (launchAppRecord.PlatformIds != null)
                    {
                        foreach (var platformIdTuple in launchAppRecord.PlatformIds)
                        {
                            if (platformIdTuple.Key != null)
                                tagContents.AppendFormat("Platform: {0}\n", platformIdTuple.Key);
                            if (platformIdTuple.Value != null)
                                tagContents.AppendFormat("App ID: {0}\n", platformIdTuple.Value);
                        }
                    }
                }
                else if (specializedType == typeof(NdefMimeImageRecordBase))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract Image record info
                    var imgRecord = new NdefMimeImageRecord(record);
                    tagContents.Append("-> MIME / Image record" + Environment.NewLine);
                    _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => SetStatusImage(await imgRecord.GetImageAsBitmap()));

                }
                else
                {
                    // Other type, not handled by this demo
                    tagContents.Append("NDEF record not parsed by this demo app" + Environment.NewLine);
                }
            }
        }

        private Rect GetElementRect(FrameworkElement element)
        {
            var elementTransform = element.TransformToVisual(null);
            var point = elementTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }
        #endregion

        #region NFC Publishing
        // ----------------------------------------------------------------------------------------------------

        private void BtnWriteLaunchApp_Click(object sender, RoutedEventArgs e)
        {
            // Create a new LaunchApp record to launch this app
            // The app will print the arguments when it is launched (see MainPage.OnNavigatedTo() method)
            var record = new NdefLaunchAppRecord { Arguments = "Hello World" };

            // WindowsPhone is the pre-defined platform ID for WP8.
            // You can get the application ID from the WMAppManifest.xml file
            //record.AddPlatformAppId("WindowsPhone", "{544ec154-b521-4d73-9405-963830adb213}");

            // The app platform for a Windows 8 computer is Windows. 
            // The format of the proximity app Id is <package family name>!<app Id>. 
            // You can get the package family name from the Windows.ApplicationModel.Package.Current.Id.FamilyName property. 
            // You must copy the app Id value from the Id attribute of the Application element in the 
            // package manifest for your app.

            var familyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            //var appId = Windows.ApplicationModel.Store.CurrentApp.AppId;    // Crashes when app is not installed from the app store!
            var appId = "8bf48432-f9c8-48cd-a014-c44d868347dc";
            // Issue on Windows 10: http://stackoverflow.com/questions/34221812/how-to-launch-my-app-via-nfc-tag
            // Issue on Windows 10: https://social.msdn.microsoft.com/Forums/sqlserver/en-US/c9653f06-0d48-498f-9b3e-335435780fd4/cw81windows-81-app-license-error-0x803f6107?forum=wpdevelop
            record.AddPlatformAppId("Windows", "{" + familyName + "!" + appId + "}");
            record.AddPlatformAppId("WindowsPhone", appId);

            // Publish the record using the proximity device
            PublishRecord(record, true);
        }

        private void BtnWriteBusinessCard_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contact
            {
                FirstName = "Andreas",
                LastName = "Jakl"
            };
            // Add the personal email address to the Contact object’s emails vector
            var personalEmail = new ContactEmail { Address = "andreas.jakl@live.com", Kind = ContactEmailKind.Work };
            contact.Emails.Add(personalEmail);

            // Adds the home phone number to the Contact object’s phones vector
            var homePhone = new ContactPhone { Number = "+1234", Kind = ContactPhoneKind.Home };
            contact.Phones.Add(homePhone);

            // Adds the address to the Contact object’s addresses vector
            var workAddress = new ContactAddress
            {
                StreetAddress = "Street 1",
                Locality = "Vienna",
                Region = "Austria",
                PostalCode = "1234",
                Kind = ContactAddressKind.Work
            };
            contact.Addresses.Add(workAddress);

            contact.Websites.Add(new ContactWebsite { Uri = new Uri("http://www.nfcinteractor.com/") });

            contact.JobInfo.Add(new ContactJobInfo
            {
                CompanyName = "Andreas Jakl",
                Title = "Mobility Evangelist"
            });
            contact.Notes = "Developer of the NFC Library";

            var record = new NdefVcardRecord(contact);

            // Publish the record using the proximity device
            PublishRecord(record, true);
        }


        private void BtnWriteMailTo_Click(object sender, RoutedEventArgs e)
        {
            // Create a new mailto record, set the relevant properties for the email
            var record = new NdefMailtoRecord
            {
                Address = "andreas.jakl@live.com",
                Subject = "Feedback for the NDEF Library",
                Body = "I think the NDEF library is ..."
            };
            // Publish the record using the proximity device
            PublishRecord(record, true);
        }

        private async void BtnWriteImageTo_Click(object sender, RoutedEventArgs e)
        {
            // Load an image
            // Note: Use supplied demo images from /Assets/DemoImages (copy these to the device)
            // Normal photos will most likely be too large for NFC tags.

            var openPicker = new Windows.Storage.Pickers.FileOpenPicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary,
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail
            };

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            var file = await openPicker.PickSingleFileAsync();

            // file is null if user cancels the file picker.
            if (file != null)
            {
                var record = await NdefMimeImageRecord.CreateFromFile(file);
                // Publish the record using the proximity device
                PublishRecord(record, true);
            }
        }

        private void BtnWriteMaps_Click(object sender, RoutedEventArgs e)
        {
            // Create a Maps record
            var record = new NdefGeoRecord
            {
                GeoType = NdefGeoRecord.NfcGeoType.MsDriveTo,
                Latitude = 48.208415,
                Longitude = 16.371282,
                PlaceTitle = "Vienna, Austria"
            };
            // Publish the record using the proximity device
            PublishRecord(record, true);
        }


        private void BtnWriteWindowsSettings_Click(object sender, RoutedEventArgs e)
        {
            // Create a Windows 10 Settings record
            var record = new NdefWindowsSettingsRecord { SettingsApp = NdefWindowsSettingsRecord.NfcSettingsApp.DevicesNfc };
            // Publish the record using the proximity device
            PublishRecord(record, true);
        }

        private void BtnPublishUri_Click(object sender, RoutedEventArgs e)
        {
            // Create a URI record
            var record = new NdefUriRecord { Uri = "http://www.nfcinteractor.com/" };
            // Publish the record using the proximity device
            PublishRecord(record, false);
        }

        private void BtnLockTag_Click(object sender, RoutedEventArgs e)
        {
            if (_device == null) return;
            // Make sure we're not already publishing another message
            StopPublishingMessage(false);
            // Start locking tags
            try
            {
                var empty = new byte[0];
                _publishingMessageId = _device.PublishBinaryMessage("SetTagReadOnly", empty.AsBuffer(), TagLockedHandler);
                // Update status text for UI
                SetStatusOutput(string.Format(_loader.GetString("StatusLockingTag")));
                // Update enabled / disabled state of buttons in the User Interface
                UpdateUiForNfcStatusAsync();
            }
            catch (Exception ex)
            {
                SetStatusOutput(string.Format(_loader.GetString("StatusLockingNotSupported"), ex.Message));
            }
        }

        private void TagLockedHandler(ProximityDevice sender, long messageid)
        {
            
        }

        private void PublishRecord(NdefRecord record, bool writeToTag)
        {
            if (_device == null) return;
            // Make sure we're not already publishing another message
            StopPublishingMessage(false);
            // Wrap the NDEF record into an NDEF message
            var message = new NdefMessage { record };
            // Convert the NDEF message to a byte array
            var msgArray = message.ToByteArray();
            try
            {
                // Publish the NDEF message to a tag or to another device, depending on the writeToTag parameter
                // Save the publication ID so that we can cancel publication later
                _publishingMessageId = _device.PublishBinaryMessage((writeToTag ? "NDEF:WriteTag" : "NDEF"), msgArray.AsBuffer(), MessageWrittenHandler);
                // Update status text for UI
                SetStatusOutput(string.Format(_loader.GetString(writeToTag ? "StatusWriteToTag" : "StatusWriteToDevice"), msgArray.Length, _publishingMessageId));
                // Update enabled / disabled state of buttons in the User Interface
                UpdateUiForNfcStatusAsync();
            }
            catch (Exception e)
            {
                SetStatusOutput(string.Format(_loader.GetString("StatusPublicationError"), e.Message));
            }
        }

        private void MessageWrittenHandler(ProximityDevice sender, long messageid)
        {
            // Stop publishing the message
            StopPublishingMessage(false);
            // Update status text for UI
            SetStatusOutput(_loader.GetString("StatusMessageWritten"));
        }
        #endregion

        #region Managing Subscriptions
        private void BtnStopSubscription_Click(object sender, RoutedEventArgs e)
        {
            // Stop NDEF subscription and print status update to the UI
            StopSubscription(true);
        }

        private void StopSubscription(bool writeToStatusOutput)
        {
            if (_subscriptionIdNdef != 0 && _device != null)
            {
                // Ask the proximity device to stop subscribing for NDEF messages
                _device.StopSubscribingForMessage(_subscriptionIdNdef);
                _subscriptionIdNdef = 0;
                // Update enabled / disabled state of buttons in the User Interface
                UpdateUiForNfcStatusAsync();
                // Update status text for UI - only if activated
                if (writeToStatusOutput) SetStatusOutput(_loader.GetString("StatusSubscriptionStopped"));
            }
        }

        private void BtnStopPublication_Click(object sender, RoutedEventArgs e)
        {
            StopPublishingMessage(true);
        }

        private void StopPublishingMessage(bool writeToStatusOutput)
        {
            if (_publishingMessageId != 0 && _device != null)
            {
                // Stop publishing the message
                _device.StopPublishingMessage(_publishingMessageId);
                _publishingMessageId = 0;
                // Update enabled / disabled state of buttons in the User Interface
                UpdateUiForNfcStatusAsync();
                // Update status text for UI - only if activated
                if (writeToStatusOutput) SetStatusOutput(_loader.GetString("StatusPublicationStopped"));
            }
        }
        #endregion

        #region UI Management

        private void SetStatusOutput(string newStatus)
        {
            // Update the status output UI element in the UI thread
            // (some of the callbacks are in a different thread that wouldn't be allowed
            // to modify the UI thread)
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { if (StatusOutput != null) StatusOutput.Text = newStatus; });
        }

        private void SetStatusImage(WriteableBitmap newImg)
        {
            // Update the status output UI element in the UI thread
            // (some of the callbacks are in a different thread that wouldn't be allowed
            // to modify the UI thread)
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                StatusImg.Source = newImg;
                if (newImg != null)
                {
                    StatusImg.Width = newImg.PixelWidth;
                    StatusImg.Height = newImg.PixelHeight;
                }
            });
        }

        private string ConvertTypeNameFormatToString(NdefRecord.TypeNameFormatType tnf)
        {
            // Each record contains a type name format, which defines which format
            // the type name is actually in.
            // This method converts the constant to a human-readable string.
            string tnfString;
            switch (tnf)
            {
                case NdefRecord.TypeNameFormatType.Empty:
                    tnfString = "Empty NDEF record (does not contain a payload)";
                    break;
                case NdefRecord.TypeNameFormatType.NfcRtd:
                    tnfString = "NFC RTD Specification";
                    break;
                case NdefRecord.TypeNameFormatType.Mime:
                    tnfString = "RFC 2046 (Mime)";
                    break;
                case NdefRecord.TypeNameFormatType.Uri:
                    tnfString = "RFC 3986 (Url)";
                    break;
                case NdefRecord.TypeNameFormatType.ExternalRtd:
                    tnfString = "External type name";
                    break;
                case NdefRecord.TypeNameFormatType.Unknown:
                    tnfString = "Unknown record type; should be treated similar to content with MIME type 'application/octet-stream' without further context";
                    break;
                case NdefRecord.TypeNameFormatType.Unchanged:
                    tnfString = "Unchanged (partial record)";
                    break;
                case NdefRecord.TypeNameFormatType.Reserved:
                    tnfString = "Reserved";
                    break;
                default:
                    tnfString = "Unknown";
                    break;
            }
            return tnfString;
        }

        private async void UpdateUiForNfcStatusAsync()
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 BtnInitNfc.IsEnabled = (_device == null);

                 // Subscription buttons
                 BtnSubscribeNdef.IsEnabled = (_device != null && _subscriptionIdNdef == 0);
                 BtnStopSubscription.IsEnabled = (_device != null && _subscriptionIdNdef != 0);

                 // Publishing buttons
                 BtnWriteBusinessCard.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnWriteMailTo.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnWriteImage.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnWriteMaps.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnWriteWindowsSettings.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnPublishUri.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnWriteLaunchApp.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnLockTag.IsEnabled = (_device != null && _publishingMessageId == 0);
                 BtnStopPublication.IsEnabled = (_device != null && _publishingMessageId != 0);
             });
        }
        #endregion

        private void AboutButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

    }
}
