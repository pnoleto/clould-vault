﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cloudVault {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.12.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"["".jpg"", "".jpeg"", "".gif"", "".mp3"", "".m4a"", "".wav"","".pdf"", "".raw"", "".bat"", "".json"", "".doc"", "".txt"","".png"", "".cs"", "".c"", "".java"", "".h"", "".rar"", "".zip"","".7zip"", "".doc"", "".docx"", "".xls"", "".xlsx"", "".ppt"","".pptx"", "".odt"", "".csv"", "".sql"", "".mdb"", "".sln"","".php"", "".asp"", "".aspx"", "".html"", "".xml"", "".psd"","".xhtml"", "".odt"", "".ods"", "".wma"", "".wav"", "".mpa"","".ogg"", "".arj"", "".deb"", "".pkg"", "".rar"", "".tar"","".gz"", "".zip"", "".py"", "".pl"", "".bin"", "".ai"" ,"".ico"","".asp"", "".aspx"", "".css"", "".js"", "".py"", "".sh"", "".vb"",""java"", "".cpp"", "".cert"", "".pem"", "".yaml"", "".yml"", "".drawio"", "".odg"", "".xsl"","".md"", "".archimate""]")]
        public string ALLOWED_EXTENSIONS {
            get {
                return ((string)(this["ALLOWED_EXTENSIONS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[\"A\",\"B\",\"C\",\"D\",\"E\",\"F\",\"G\",\"H\",\"I\",\"J\",\"K\",\"L\",\"M\",\"N\",\"O\",\"P\",\"Q\",\"R\",\"S\",\"T\"," +
            "\"U\",\"V\",\"W\",\"X\",\"Y\",\"Z\",\"0\",\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\",\"8\",\"9\",\"$\",\"%\",\"#\",\"@\"," +
            "\"*\",\"&\",\"+\",\"-\",\"[\",\"]\",\"?\",\"/\"]")]
        public string CHARS {
            get {
                return ((string)(this["CHARS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\".protected\"")]
        public string DEFAULT_EXTENSION {
            get {
                return ((string)(this["DEFAULT_EXTENSION"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[\"WinSxS\", \"Windows\",  \"System32\", \"Program Files\"]")]
        public string IGNORED_PATHS {
            get {
                return ((string)(this["IGNORED_PATHS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public int ITERACTIONS_LIMIT {
            get {
                return ((int)(this["ITERACTIONS_LIMIT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("128")]
        public int KEY_SIZE {
            get {
                return ((int)(this["KEY_SIZE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("64")]
        public int SALT_SIZE {
            get {
                return ((int)(this["SALT_SIZE"]));
            }
        }
    }
}