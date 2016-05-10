﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NdefLibrary.Ndef {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class NdefExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NdefExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NdefLibrary.Ndef.NdefExceptionMessages", typeof(NdefExceptionMessages).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown format for class of device information..
        /// </summary>
        public static string ExBtCodUnknownFormat {
            get {
                return ResourceManager.GetString("ExBtCodUnknownFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encoded Bluetooth OOB data length does not match payload length..
        /// </summary>
        public static string ExBtInvalidLength {
            get {
                return ResourceManager.GetString("ExBtInvalidLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid length of the simple pairing hash C (should be 16 octets)..
        /// </summary>
        public static string ExBtInvalidLengthSimplePairingHashC {
            get {
                return ResourceManager.GetString("ExBtInvalidLengthSimplePairingHashC", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid length of the simple pairing randomizer R (should be 16 octets)..
        /// </summary>
        public static string ExBtInvalidLengthSimplePairingRandomizerR {
            get {
                return ResourceManager.GetString("ExBtInvalidLengthSimplePairingRandomizerR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OOB data does not reach minumum length of 8 bytes..
        /// </summary>
        public static string ExBtInvalidMinimumLength {
            get {
                return ResourceManager.GetString("ExBtInvalidMinimumLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bluetooth Device address must be defined..
        /// </summary>
        public static string ExBtNoDeviceAddress {
            get {
                return ResourceManager.GetString("ExBtNoDeviceAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No valid Bluetooth address in hexadecimal format..
        /// </summary>
        public static string ExBtNoValidHexAddress {
            get {
                return ResourceManager.GetString("ExBtNoValidHexAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No valid Bluetooth Service Class ID..
        /// </summary>
        public static string ExBtNoValidServiceClassId {
            get {
                return ResourceManager.GetString("ExBtNoValidServiceClassId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bluetooth Service Class Length invalid..
        /// </summary>
        public static string ExBtNoValidServiceClassLength {
            get {
                return ResourceManager.GetString("ExBtNoValidServiceClassLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HERE Maps URI schemes require an AppId..
        /// </summary>
        public static string ExGeoNoHereMapsAppId {
            get {
                return ResourceManager.GetString("ExGeoNoHereMapsAppId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Carrier Data Reference present in Handover Select / Alternative Carrier record..
        /// </summary>
        public static string ExHandoverAcCarrierNoData {
            get {
                return ResourceManager.GetString("ExHandoverAcCarrierNoData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data Reference is too long - use the record ID in max 255 characters..
        /// </summary>
        public static string ExHandoverDataReferenceTooLong {
            get {
                return ResourceManager.GetString("ExHandoverDataReferenceTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Too many auxiliary data references - use max 255 data references..
        /// </summary>
        public static string ExHandoverDataTooManyAuxiliaryReferences {
            get {
                return ResourceManager.GetString("ExHandoverDataTooManyAuxiliaryReferences", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid error data..
        /// </summary>
        public static string ExHandoverErrorInvalidData {
            get {
                return ResourceManager.GetString("ExHandoverErrorInvalidData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not create Handover Error record based on the provided source data..
        /// </summary>
        public static string ExHandoverErrorInvalidSourceData {
            get {
                return ResourceManager.GetString("ExHandoverErrorInvalidSourceData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No error data present..
        /// </summary>
        public static string ExHandoverErrorNoData {
            get {
                return ResourceManager.GetString("ExHandoverErrorNoData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No error reason present..
        /// </summary>
        public static string ExHandoverErrorNoReason {
            get {
                return ResourceManager.GetString("ExHandoverErrorNoReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown error reason..
        /// </summary>
        public static string ExHandoverErrorUnknownReason {
            get {
                return ResourceManager.GetString("ExHandoverErrorUnknownReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Handover version must be one byte, containing major and minor fields..
        /// </summary>
        public static string ExHandoverInvalidVersion {
            get {
                return ResourceManager.GetString("ExHandoverInvalidVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Alternative Carrier record needs to define a carrier data reference..
        /// </summary>
        public static string ExHandoverNoCarrierDataReference {
            get {
                return ResourceManager.GetString("ExHandoverNoCarrierDataReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not create Handover Select message - found illegal record..
        /// </summary>
        public static string ExHandoverSelectMsgInvalidRecords {
            get {
                return ResourceManager.GetString("ExHandoverSelectMsgInvalidRecords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No calendar data found in the supplied calendar data..
        /// </summary>
        public static string ExIcalendarNoCalendarFound {
            get {
                return ResourceManager.GetString("ExIcalendarNoCalendarFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The supplied calendar information does not contain a valid event / appointment..
        /// </summary>
        public static string ExIcalendarNoEventFound {
            get {
                return ResourceManager.GetString("ExIcalendarNoEventFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to instantiate new record from incompatible record..
        /// </summary>
        public static string ExInvalidCopy {
            get {
                return ResourceManager.GetString("ExInvalidCopy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Arguments for LaunchApp must not be empty..
        /// </summary>
        public static string ExLaunchAppArgumentsEmpty {
            get {
                return ResourceManager.GetString("ExLaunchAppArgumentsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to assemble LaunchApp payload: length of platform / AppID tuple more than 255 characters..
        /// </summary>
        public static string ExLaunchAppPlatformLength {
            get {
                return ResourceManager.GetString("ExLaunchAppPlatformLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to assemble LaunchApp payload: at least one platform / AppID tuple is required..
        /// </summary>
        public static string ExLaunchAppPlatformMissing {
            get {
                return ResourceManager.GetString("ExLaunchAppPlatformMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At least one platform / app ID tuple needs to be defined..
        /// </summary>
        public static string ExLaunchAppPlatformsEmpty {
            get {
                return ResourceManager.GetString("ExLaunchAppPlatformsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For a Windows Phone platform ID, please supply the 36 character product ID containing only hexadecimal characters and &apos;-&apos;..
        /// </summary>
        public static string ExLaunchAppWpId {
            get {
                return ResourceManager.GetString("ExLaunchAppWpId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recipient address must not be empty..
        /// </summary>
        public static string ExMailtoAddressEmpty {
            get {
                return ResourceManager.GetString("ExMailtoAddressEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The recipient address does not appear to be valid..
        /// </summary>
        public static string ExMailtoAddressNotValid {
            get {
                return ResourceManager.GetString("ExMailtoAddressNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Message Begin flag present after first record..
        /// </summary>
        public static string ExMessageBeginLate {
            get {
                return ResourceManager.GetString("ExMessageBeginLate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Expected Message Begin missing in first record..
        /// </summary>
        public static string ExMessageBeginMissing {
            get {
                return ResourceManager.GetString("ExMessageBeginMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Message End flag present after last record..
        /// </summary>
        public static string ExMessageEndLate {
            get {
                return ResourceManager.GetString("ExMessageEndLate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Invalid chunked data, Id Length != 0..
        /// </summary>
        public static string ExMessageInvalidChunkedId {
            get {
                return ResourceManager.GetString("ExMessageInvalidChunkedId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Invalid chunked data, Type Length != 0..
        /// </summary>
        public static string ExMessageInvalidChunkedType {
            get {
                return ResourceManager.GetString("ExMessageInvalidChunkedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Malformed NDEF Message, missing begin or end..
        /// </summary>
        public static string ExMessageNoBeginOrEnd {
            get {
                return ResourceManager.GetString("ExMessageNoBeginOrEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Partial chunk not empty or typeNameFormat not 0x06 as expected..
        /// </summary>
        public static string ExMessagePartialChunk {
            get {
                return ResourceManager.GetString("ExMessagePartialChunk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ndef parse error: Unexpected end of message..
        /// </summary>
        public static string ExMessageUnexpectedEnd {
            get {
                return ResourceManager.GetString("ExMessageUnexpectedEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Language code must be 5 characters..
        /// </summary>
        public static string ExNearSpeakLanguageWrongLength {
            get {
                return ResourceManager.GetString("ExNearSpeakLanguageWrongLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Online ID must be set..
        /// </summary>
        public static string ExNearSpeakNoCloudId {
            get {
                return ResourceManager.GetString("ExNearSpeakNoCloudId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Language code must be defined..
        /// </summary>
        public static string ExNearSpeakNoLanguage {
            get {
                return ResourceManager.GetString("ExNearSpeakNoLanguage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text to speak must not be empty..
        /// </summary>
        public static string ExNearSpeakNoText {
            get {
                return ResourceManager.GetString("ExNearSpeakNoText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No valid NearSpeak type identifier..
        /// </summary>
        public static string ExNearSpeakNoValidType {
            get {
                return ResourceManager.GetString("ExNearSpeakNoValidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ProductName for Nokia Accessories record must not be empty..
        /// </summary>
        public static string ExNokiaAccessoriesProductEmpty {
            get {
                return ResourceManager.GetString("ExNokiaAccessoriesProductEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Serial number for Nokia Accessories must not be empty..
        /// </summary>
        public static string ExNokiaAccessoriesSerialEmpty {
            get {
                return ResourceManager.GetString("ExNokiaAccessoriesSerialEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Serial number must be 19 chars, decimal numbers only..
        /// </summary>
        public static string ExNokiaAccessoriesSerialFormat {
            get {
                return ResourceManager.GetString("ExNokiaAccessoriesSerialFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The raw URI can only be retrieved if the URI is not abbreviated by the record..
        /// </summary>
        public static string ExRawUriNoAbbreviation {
            get {
                return ResourceManager.GetString("ExRawUriNoAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Empty record must not have a payload or a type..
        /// </summary>
        public static string ExRecordEmptyWithTypeOrPayload {
            get {
                return ResourceManager.GetString("ExRecordEmptyWithTypeOrPayload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Type set for record..
        /// </summary>
        public static string ExRecordNoType {
            get {
                return ResourceManager.GetString("ExRecordNoType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id must not be set for middle or terminating record (TNF = Unchanged)..
        /// </summary>
        public static string ExRecordUnchangedId {
            get {
                return ResourceManager.GetString("ExRecordUnchangedId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Record with TNF Unchanged must have an empty type name..
        /// </summary>
        public static string ExRecordUnchangedTypeName {
            get {
                return ResourceManager.GetString("ExRecordUnchangedTypeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Body text for SMS record must not be empty..
        /// </summary>
        public static string ExSmsBodyEmpty {
            get {
                return ResourceManager.GetString("ExSmsBodyEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number for SMS record must not be empty..
        /// </summary>
        public static string ExSmsNumberEmpty {
            get {
                return ResourceManager.GetString("ExSmsNumberEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Social user name must not be empty..
        /// </summary>
        public static string ExSocialNoUser {
            get {
                return ResourceManager.GetString("ExSocialNoUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Uri of Smart Poster ust not be empty..
        /// </summary>
        public static string ExSpUriEmpty {
            get {
                return ResourceManager.GetString("ExSpUriEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number for telephone record must not be empty..
        /// </summary>
        public static string ExTelNumberEmpty {
            get {
                return ResourceManager.GetString("ExTelNumberEmpty", resourceCulture);
            }
        }
    }
}
