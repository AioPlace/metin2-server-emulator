#region License

//     This file is part of Metin 2 Server Emulator.
// 
//     Metin 2 Server Emulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Metin 2 Server Emulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Metin 2 Server Emulator.  If not, see <http://www.gnu.org/licenses/>

#endregion

using System.Collections;

namespace Metin2AuthServerEmulator.Services
{
    internal static class AuthQueue
    {
        private static readonly Hashtable Queue = new Hashtable();

        /// <summary>
        ///   Adds an user to the Auth Queue
        /// </summary>
        /// <param name="username"> Username </param>
        /// <param name="sessionId"> Session ID </param>
        public static bool Add(string username, uint sessionId)
        {
            if (Queue.ContainsKey(username))
            {
                AuthInfo a = (AuthInfo) Queue[username];

                if (!a.IsLoggedIn)
                    Queue.Remove(username);
                else
                    return false;
            }

            Queue.Add(username, new AuthInfo(sessionId, false));

            return true;
        }

        /// <summary>
        ///   Removes an user from the Auth Queue
        /// </summary>
        /// <param name="username"> Username </param>
        public static void Remove(string username)
        {
            if (!Queue.ContainsKey(username))
                return;

            Queue.Remove(username);
        }

        /// <summary>
        ///   Sets an user Logged In
        /// </summary>
        /// <param name="username"> Username </param>
        public static void LogIn(string username)
        {
            if (!Queue.ContainsKey(username))
                return;

            AuthInfo info = (AuthInfo) Queue[username];
            info.IsLoggedIn = true;
        }

        /// <summary>
        ///   Sets an user Logged Out
        /// </summary>
        /// <param name="username"> Username </param>
        public static void LogOut(string username)
        {
            if (!Queue.ContainsKey(username))
                return;

            AuthInfo info = (AuthInfo) Queue[username];
            info.IsLoggedIn = false;
        }

        /// <summary>
        ///   Checks authorization key for an username
        /// </summary>
        /// <param name="username"> Username </param>
        /// <param name="sessionId"> Session id </param>
        /// <returns> If authentication key is Valid </returns>
        public static bool CheckAuth(string username, uint sessionId)
        {
            AuthInfo info = (AuthInfo) Queue[username];
            return (Queue.ContainsKey(username) &&
                    (info.SessionId == sessionId) &&
                    (!info.IsLoggedIn));
        }

        /// <summary>
        ///   Checks if a user is already logged in
        /// </summary>
        /// <param name="username"> Username </param>
        /// <returns> If user is logged in </returns>
        public static bool IsAlreadyLoggedIn(string username)
        {
            AuthInfo info = (AuthInfo) Queue[username];
            return (Queue.ContainsKey(username) &&
                    info.IsLoggedIn);
        }

        #region Nested type: AuthInfo

        private struct AuthInfo
        {
            public readonly uint SessionId;
            public bool IsLoggedIn;

            public AuthInfo(uint sessionId, bool isLoggedIn)
            {
                SessionId = sessionId;
                IsLoggedIn = isLoggedIn;
            }
        }

        #endregion
    }
}