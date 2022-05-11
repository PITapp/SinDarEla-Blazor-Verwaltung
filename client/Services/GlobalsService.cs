using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SinDarElaVerwaltung.Models;
using SinDarElaVerwaltung.Models.DbSinDarEla;
using Radzen;

namespace SinDarElaVerwaltung
{
    public partial class GlobalsService
    {
        public event Action<PropertyChangedEventArgs> PropertyChanged;


        string _globalBenutzerName;
        public string globalBenutzerName
        {
            get
            {
                return _globalBenutzerName;
            }
            set
            {
                if(!object.Equals(_globalBenutzerName, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "globalBenutzerName", NewValue = value, OldValue = _globalBenutzerName, IsGlobal = true };
                    _globalBenutzerName = value;
                    PropertyChanged?.Invoke(args);
                }
            }
        }

        string _globalBenutzerID;
        public string globalBenutzerID
        {
            get
            {
                return _globalBenutzerID;
            }
            set
            {
                if(!object.Equals(_globalBenutzerID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "globalBenutzerID", NewValue = value, OldValue = _globalBenutzerID, IsGlobal = true };
                    _globalBenutzerID = value;
                    PropertyChanged?.Invoke(args);
                }
            }
        }

        string _globalBenutzerBaseID;
        public string globalBenutzerBaseID
        {
            get
            {
                return _globalBenutzerBaseID;
            }
            set
            {
                if(!object.Equals(_globalBenutzerBaseID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "globalBenutzerBaseID", NewValue = value, OldValue = _globalBenutzerBaseID, IsGlobal = true };
                    _globalBenutzerBaseID = value;
                    PropertyChanged?.Invoke(args);
                }
            }
        }
    }

    public class PropertyChangedEventArgs
    {
        public string Name { get; set; }
        public object NewValue { get; set; }
        public object OldValue { get; set; }
        public bool IsGlobal { get; set; }
    }
}
