﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.6387
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.6387.
// 
#pragma warning disable 1591

namespace InterLinkClass.AirTelMoney {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ThirdpartyWebServicesPortBinding", Namespace="http://WebServices.BillerCore/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(operationReturn))]
    public partial class ThirdpartyWebServices : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback PayOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerifyWaridpesaCustomerOperationCompleted;
        
        private System.Threading.SendOrPostCallback ProcessWithdrawalOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ThirdpartyWebServices() {
            this.Url = global::InterLinkClass.Properties.Settings.Default.InterLinkClass_AirTelMoney_ThirdpartyWebServices;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event PayOutCompletedEventHandler PayOutCompleted;
        
        /// <remarks/>
        public event VerifyWaridpesaCustomerCompletedEventHandler VerifyWaridpesaCustomerCompleted;
        
        /// <remarks/>
        public event ProcessWithdrawalCompletedEventHandler ProcessWithdrawalCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://WebServices.BillerCore/", ResponseNamespace="http://WebServices.BillerCore/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public payoutResponse PayOut([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string BillerCode, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string PayoutPhone, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string PayoutPhonePIN, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string TransactionSignature, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Recipient, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Amount, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string PaymentMemo, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string RequestId) {
            object[] results = this.Invoke("PayOut", new object[] {
                        BillerCode,
                        PayoutPhone,
                        PayoutPhonePIN,
                        TransactionSignature,
                        Recipient,
                        Amount,
                        PaymentMemo,
                        RequestId});
            return ((payoutResponse)(results[0]));
        }
        
        /// <remarks/>
        public void PayOutAsync(string BillerCode, string PayoutPhone, string PayoutPhonePIN, string TransactionSignature, string Recipient, string Amount, string PaymentMemo, string RequestId) {
            this.PayOutAsync(BillerCode, PayoutPhone, PayoutPhonePIN, TransactionSignature, Recipient, Amount, PaymentMemo, RequestId, null);
        }
        
        /// <remarks/>
        public void PayOutAsync(string BillerCode, string PayoutPhone, string PayoutPhonePIN, string TransactionSignature, string Recipient, string Amount, string PaymentMemo, string RequestId, object userState) {
            if ((this.PayOutOperationCompleted == null)) {
                this.PayOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPayOutOperationCompleted);
            }
            this.InvokeAsync("PayOut", new object[] {
                        BillerCode,
                        PayoutPhone,
                        PayoutPhonePIN,
                        TransactionSignature,
                        Recipient,
                        Amount,
                        PaymentMemo,
                        RequestId}, this.PayOutOperationCompleted, userState);
        }
        
        private void OnPayOutOperationCompleted(object arg) {
            if ((this.PayOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PayOutCompleted(this, new PayOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://WebServices.BillerCore/", ResponseNamespace="http://WebServices.BillerCore/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public customerVerificationResponse VerifyWaridpesaCustomer([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string BillerCode, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string CustomerPhone, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string TransactionSignature) {
            object[] results = this.Invoke("VerifyWaridpesaCustomer", new object[] {
                        BillerCode,
                        CustomerPhone,
                        TransactionSignature});
            return ((customerVerificationResponse)(results[0]));
        }
        
        /// <remarks/>
        public void VerifyWaridpesaCustomerAsync(string BillerCode, string CustomerPhone, string TransactionSignature) {
            this.VerifyWaridpesaCustomerAsync(BillerCode, CustomerPhone, TransactionSignature, null);
        }
        
        /// <remarks/>
        public void VerifyWaridpesaCustomerAsync(string BillerCode, string CustomerPhone, string TransactionSignature, object userState) {
            if ((this.VerifyWaridpesaCustomerOperationCompleted == null)) {
                this.VerifyWaridpesaCustomerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerifyWaridpesaCustomerOperationCompleted);
            }
            this.InvokeAsync("VerifyWaridpesaCustomer", new object[] {
                        BillerCode,
                        CustomerPhone,
                        TransactionSignature}, this.VerifyWaridpesaCustomerOperationCompleted, userState);
        }
        
        private void OnVerifyWaridpesaCustomerOperationCompleted(object arg) {
            if ((this.VerifyWaridpesaCustomerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerifyWaridpesaCustomerCompleted(this, new VerifyWaridpesaCustomerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://WebServices.BillerCore/", ResponseNamespace="http://WebServices.BillerCore/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public cashDispenseResponse ProcessWithdrawal([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string BillerCode, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string RetailerAccount, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string RetailerAccountPIN, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string TransactionSignature, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string CustomerPhone, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string SecretCode, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string RequestId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Amount) {
            object[] results = this.Invoke("ProcessWithdrawal", new object[] {
                        BillerCode,
                        RetailerAccount,
                        RetailerAccountPIN,
                        TransactionSignature,
                        CustomerPhone,
                        SecretCode,
                        RequestId,
                        Amount});
            return ((cashDispenseResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ProcessWithdrawalAsync(string BillerCode, string RetailerAccount, string RetailerAccountPIN, string TransactionSignature, string CustomerPhone, string SecretCode, string RequestId, string Amount) {
            this.ProcessWithdrawalAsync(BillerCode, RetailerAccount, RetailerAccountPIN, TransactionSignature, CustomerPhone, SecretCode, RequestId, Amount, null);
        }
        
        /// <remarks/>
        public void ProcessWithdrawalAsync(string BillerCode, string RetailerAccount, string RetailerAccountPIN, string TransactionSignature, string CustomerPhone, string SecretCode, string RequestId, string Amount, object userState) {
            if ((this.ProcessWithdrawalOperationCompleted == null)) {
                this.ProcessWithdrawalOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProcessWithdrawalOperationCompleted);
            }
            this.InvokeAsync("ProcessWithdrawal", new object[] {
                        BillerCode,
                        RetailerAccount,
                        RetailerAccountPIN,
                        TransactionSignature,
                        CustomerPhone,
                        SecretCode,
                        RequestId,
                        Amount}, this.ProcessWithdrawalOperationCompleted, userState);
        }
        
        private void OnProcessWithdrawalOperationCompleted(object arg) {
            if ((this.ProcessWithdrawalCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProcessWithdrawalCompleted(this, new ProcessWithdrawalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.6387")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://WebServices.BillerCore/")]
    public partial class payoutResponse : operationReturn {
        
        private string transactionIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string transactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(payoutResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(cashDispenseResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(customerVerificationResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.6387")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://WebServices.BillerCore/")]
    public partial class operationReturn {
        
        private int returnCodeField;
        
        private bool returnCodeFieldSpecified;
        
        private string returnMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int returnCode {
            get {
                return this.returnCodeField;
            }
            set {
                this.returnCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool returnCodeSpecified {
            get {
                return this.returnCodeFieldSpecified;
            }
            set {
                this.returnCodeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string returnMessage {
            get {
                return this.returnMessageField;
            }
            set {
                this.returnMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.6387")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://WebServices.BillerCore/")]
    public partial class cashDispenseResponse : operationReturn {
        
        private string amountField;
        
        private System.DateTime dateInitiatedField;
        
        private bool dateInitiatedFieldSpecified;
        
        private string transactionIdField;
        
        private string withdrawalStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime dateInitiated {
            get {
                return this.dateInitiatedField;
            }
            set {
                this.dateInitiatedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dateInitiatedSpecified {
            get {
                return this.dateInitiatedFieldSpecified;
            }
            set {
                this.dateInitiatedFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string transactionId {
            get {
                return this.transactionIdField;
            }
            set {
                this.transactionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string withdrawalStatus {
            get {
                return this.withdrawalStatusField;
            }
            set {
                this.withdrawalStatusField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.6387")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://WebServices.BillerCore/")]
    public partial class customerVerificationResponse : operationReturn {
        
        private string customerNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string customerName {
            get {
                return this.customerNameField;
            }
            set {
                this.customerNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    public delegate void PayOutCompletedEventHandler(object sender, PayOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PayOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PayOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public payoutResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((payoutResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    public delegate void VerifyWaridpesaCustomerCompletedEventHandler(object sender, VerifyWaridpesaCustomerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerifyWaridpesaCustomerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerifyWaridpesaCustomerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public customerVerificationResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((customerVerificationResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    public delegate void ProcessWithdrawalCompletedEventHandler(object sender, ProcessWithdrawalCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ProcessWithdrawalCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ProcessWithdrawalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public cashDispenseResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((cashDispenseResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591