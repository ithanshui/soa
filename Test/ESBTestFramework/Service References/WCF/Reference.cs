﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESB.TestFramework.WCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mb.com", ConfigurationName="WCF.IEsbAction")]
    public interface IEsbAction {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://mb.com/EsbAction", ReplyAction="http://mb.com/IEsbAction/EsbActionResponse")]
        string EsbAction(string action, string request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEsbActionChannel : ESB.TestFramework.WCF.IEsbAction, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EsbActionClient : System.ServiceModel.ClientBase<ESB.TestFramework.WCF.IEsbAction>, ESB.TestFramework.WCF.IEsbAction {
        
        public EsbActionClient() {
        }
        
        public EsbActionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EsbActionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EsbActionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EsbActionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string EsbAction(string action, string request) {
            return base.Channel.EsbAction(action, request);
        }
    }
}
