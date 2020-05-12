﻿using System;

namespace EmbedIO.Internal
{
    internal static class UriUtility
    {
        // Returns true if string starts with "http:", "https:", "ws:", or "wss:"
        public static bool CanBeAbsoluteUrl(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            switch (str[0])
            {
                case 'h':
                    if (str.Length < 5)
                        return false;
                    if (str[1] != 't' || str[2] != 't' || str[3] != 'p')
                        return false;
                    return str[4] switch {
                        ':' => true,
                        's' => str.Length >= 6 && str[5] == ':',
                        _ => false,
                    };

                case 'w':
                    if (str.Length < 3)
                        return false;
                    if (str[1] != 's')
                        return false;
                    return str[2] switch {
                        ':' => true,
                        's' => str.Length >= 4 && str[3] == ':',
                        _ => false,
                    };

                default:
                    return false;
            }
        }

        public static Uri StringToUri(string str)
        {
            Uri.TryCreate(str, CanBeAbsoluteUrl(str) ? UriKind.Absolute : UriKind.Relative, out var result);
            return result;
        }

        public static Uri? StringToAbsoluteUri(string str)
        {
            if (!CanBeAbsoluteUrl(str))
                return null;

            Uri.TryCreate(str, UriKind.Absolute, out var result);
            return result;
        }
    }
}