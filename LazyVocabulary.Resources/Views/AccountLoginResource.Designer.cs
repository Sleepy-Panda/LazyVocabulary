﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LazyVocabulary.Resources.Views {
    using System;
    
    
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
    public class AccountLoginResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AccountLoginResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LazyVocabulary.Resources.Views.AccountLoginResource", typeof(AccountLoginResource).Assembly);
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
        ///   Looks up a localized string similar to Для входа введите имя пользователя или адрес электронной почты и пароль, использованные при регистрации:.
        /// </summary>
        public static string FormInstructions {
            get {
                return ResourceManager.GetString("FormInstructions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вход на сайт.
        /// </summary>
        public static string FormName {
            get {
                return ResourceManager.GetString("FormName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to * Пароль....
        /// </summary>
        public static string PasswordPlaceholder {
            get {
                return ResourceManager.GetString("PasswordPlaceholder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Восстановить пароль.
        /// </summary>
        public static string RestorePassword {
            get {
                return ResourceManager.GetString("RestorePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Войти.
        /// </summary>
        public static string SignIn {
            get {
                return ResourceManager.GetString("SignIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ...или войдите с помощью:.
        /// </summary>
        public static string SignInWith {
            get {
                return ResourceManager.GetString("SignInWith", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to * Имя пользователя или Email....
        /// </summary>
        public static string UserNameOrEmailPlaceholder {
            get {
                return ResourceManager.GetString("UserNameOrEmailPlaceholder", resourceCulture);
            }
        }
    }
}
