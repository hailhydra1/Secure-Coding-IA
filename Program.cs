using System;
using System.Security;

class Program
{
    static void Main()
    {
        SecureString securePassword = new SecureString();
        ConsoleKeyInfo key;

        Console.WriteLine("Enter your password: ");

        // Reading password input
        do
        {
            key = Console.ReadKey(true);
            if (key.KeyChar != (char)0) // Ignore null characters
            {
                securePassword.AppendChar(key.KeyChar);
            }
        } while (key.Key != ConsoleKey.Enter);

        // Convert SecureString to unmanaged memory (not recommended)
        IntPtr unmanagedPtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);

        try
        {
            // Simulate an insecure operation by exposing the password
            string exposedPassword = System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedPtr);
            Console.WriteLine("\nExposed password (should not be shown!): " + exposedPassword);
        }
        finally
        {
            // Always clean up
            System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedPtr);
            securePassword.Dispose();
            
            string exposedPassword = System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedPtr);
            Console.WriteLine("\nExposed password (after cleaning up and disposing): " + exposedPassword);
        }
    }
}