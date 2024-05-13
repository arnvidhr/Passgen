using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Passgen
{
    /// <summary>
    /// Random passwords generator class (C) 2024 Arvydas Grigonis
    /// </summary>
    internal class RandomPasswordGenerator
    {
        #region Attrs
        readonly Random Random = new();

        string[] _Args;

        bool _Validated = false;

        int _NumberOfPasswords = 0;
        int _PasswordLength = 0;

        bool _UseDigits = false;
        bool _UseUppercaseCharacters = false;
        bool _UseLowercaseCharacters = false;
        bool _UseSymbols = false;

        string _AvailableLetters = string.Empty;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args"></param>
        internal RandomPasswordGenerator(string[] args)
        {
            _Args = args;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Initialization before generation
        /// </summary>
        void Initialize()
        {
            if(string.IsNullOrEmpty(_AvailableLetters))
            {
                if(_UseDigits)
                    _AvailableLetters += GenerateString('0', '9');

                if (_UseUppercaseCharacters)
                    _AvailableLetters += GenerateString('A', 'Z');

                if (_UseLowercaseCharacters)
                    _AvailableLetters += GenerateString('a', 'z');

                if(_UseSymbols)
                {
                    _AvailableLetters += GenerateString('!', '/');
                    _AvailableLetters += GenerateString(':', '`');
                    _AvailableLetters += GenerateString('{', '~');
                }
            }
        }

        string GenerateString(char from, char to)
        {
            var retVal = string.Empty;

            for (char i = from; i < (to + 1); i++)
            {
                retVal += i;
            }
            return retVal;
        }

        /// <summary>
        /// Single password generation
        /// </summary>
        /// <returns></returns>
        string GeneratePassword()
        {
            var len = _PasswordLength;
            var chars = new char[len];

            while (--len >= 0)
                chars[len] = _AvailableLetters[Random.Next(_AvailableLetters.Length)];

            return new string(chars);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation and parsing of params
        /// </summary>
        /// <returns></returns>
        internal bool Validate()
        {
            // Args
            if (_Args.Length < 3)
                return false;

            // Number of passwords
            if (!int.TryParse(_Args[0], out _NumberOfPasswords))
                return false;

            if (_NumberOfPasswords < 1)
                return false;

            // Password length
            if (!int.TryParse(_Args[1], out _PasswordLength))
                return false;

            if(_PasswordLength < 1)
                return false;

            // duls!
            _UseDigits = _Args[2].Contains('d', StringComparison.OrdinalIgnoreCase);
            _UseUppercaseCharacters = _Args[2].Contains('u', StringComparison.OrdinalIgnoreCase);
            _UseLowercaseCharacters = _Args[2].Contains('l', StringComparison.OrdinalIgnoreCase);
            _UseSymbols = _Args[2].Contains('s', StringComparison.OrdinalIgnoreCase);

            Initialize();

            // Validated if we have some characters for generation
            _Validated = _AvailableLetters.Length > 0;

            return _Validated;
        }

        /// <summary>
        /// Generate entry point
        /// </summary>
        /// <returns></returns>
        internal List<string> Generate()
        {
            var retVal = new List<string>();

            if(_Validated)
            {
                for (int i = 0; i < _NumberOfPasswords; i++)
                {
                    retVal.Add(GeneratePassword());
                }
            }
            return retVal;
        }

        #endregion
    }
}
