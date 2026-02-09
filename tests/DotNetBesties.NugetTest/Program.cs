using DotNetBesties.Helpers;
using DotNetBesties.Helpers.Extensions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var a = "That's a test".EnsureStartsWith("Must have! ");
Console.WriteLine(a);