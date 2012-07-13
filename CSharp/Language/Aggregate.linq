<Query Kind="Statements" />

string sentence = "the quick brown fox jumps over the lazy dog";

// Split the string into individual words.
string[] words = sentence.Split( ' ' );

// Prepend each word to the beginning of the 
// new sentence to reverse the word order.
string reversed = words.Aggregate( ( workingSentence, next ) =>
									  next + " " + workingSentence );

Console.WriteLine( reversed );
Console.WriteLine("------------------------");

char[] figures = "23456789TJQKA".ToCharArray();
char[] suites = "SHDC".ToCharArray();
//List<string> deck = new List<string>();
// 
//foreach (var figure in figures) {
//	foreach (var suite in suites) {//		deck.Add(string.Format("{0}{1}", figure, suite));
//	}
//}
 
//Or, neatly
var cards = (from r in "23456789TJQKA" from s in "SHDC" select "" + r + s).ToList();

foreach(string c in cards)
 Console.Write(c);

