﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GZipTest.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GZipTest.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Удалить файл?.
        /// </summary>
        internal static string DeleteFile {
            get {
                return ResourceManager.GetString("DeleteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не удалось удалить файл.
        /// </summary>
        internal static string FailedToDeleteFile {
            get {
                return ResourceManager.GetString("FailedToDeleteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Файл был успешно удален.
        /// </summary>
        internal static string FileHasBeenDeleted {
            get {
                return ResourceManager.GetString("FileHasBeenDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Строка аргументов имела неверный формат. Правильный формат : GZipTest.exe compress/decompress [имя исходного файла] [имя результирующего файла].
        /// </summary>
        internal static string IncorrectArgsLength {
            get {
                return ResourceManager.GetString("IncorrectArgsLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не удалось прочитать режим работы.
        /// </summary>
        internal static string ModeParseError {
            get {
                return ResourceManager.GetString("ModeParseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Результирующий файл &apos;{0}&apos; уже существует.
        /// </summary>
        internal static string ResultFileExists {
            get {
                return ResourceManager.GetString("ResultFileExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Исходный файл &apos;{0}&apos; не найден, остановка выполнения программы .
        /// </summary>
        internal static string SourceFileDoesNotExist {
            get {
                return ResourceManager.GetString("SourceFileDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Продолжение работы неевозможно, программа завершается.
        /// </summary>
        internal static string UnableToProceed {
            get {
                return ResourceManager.GetString("UnableToProceed", resourceCulture);
            }
        }
    }
}
