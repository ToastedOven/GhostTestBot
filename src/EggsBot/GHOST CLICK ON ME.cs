using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostBot
{
    internal static class MyBigSexClassName //You can rename this with F2 if you want to. Do not just change it, press F2 then start typing
    {
        internal static string prefixString = "!"; //You can change this if you want, just keep track of what it is
        internal static SocketMessage incomingMessage;
        internal static void MessageReceived(SocketMessage message) //This will only happen if the author is NOT GhostTestBot (for obvious reasons) and if the message is sent to bot-commands
        {
            incomingMessage = message;
            string firstPart = message.Content.Split(' ')[0]; //Gets the first word of the message (like if we had "!add 5 4" if would set firstPart to "!add") we will use this in various places. if we had [1] instead it would set firstPart to "5". Note this is still a string even though its just a number. You could however, convert this to a number...... hint hint
            //message is passed in and usable here. Do not worry how for now, just work with the SocketMessage: "message" as it is given to you. If you want to rename it you can, just make sure to use F2
            Console.WriteLine($"[{message.Author.Username}] wrote [{message.Content.ToString()}]"); //Writes out the author's username (not server nickname) and what they wrote to the console.
            //You can get many parts from a SocketMessage item, some of the most common things you want are SocketMessage.Author and SocketMessage.Content.
            //SocketMessage.Author lets you get basically all info about who wrote the message. Keep in mind, if you want to truly identify a user you should use their Id, IE: message.Author.Id
            //SocketMessage.Content is the message as a string (so basically exactly what you see in discord)
            //SocketMessage.Attachments is a all of the files in the message (uploaded images/videos, exe files, stuff like that, don't worry about it for now but it's good to know about)


            //TODO modify the if satement below to identify messages intended for your bot and ignore all other messages. Consider using String.StartsWith to do this. You can literally google "String.StartsWith C#" and you will find what you need, stick to microsoft documentation since C# is their baby and their documentation is almost flawless.
            //I don't know how far you are with your knowledge, but just a quick rundown. "if" statements check a bool in the parameter section (inside of the () brackets) so if your statement looks like: if(1 == 2){} it will always skip it's segment, whereas if you have: if(2 == 2){} then it will run the code in it's section. So with all this in mind, for this if statement, you should check if the message starts with prefixString
            if (false/*replace "false" with a check that sees if the message is intended for bot (starts with prefixString)*/)
            {
                Console.WriteLine($"the message started with {prefixString} and is probably intended for the bot");

                //TODO so now that we know the message started with the prefix (default is !) we want to check which command was inputted (!add, !multiply, and stuff like that)
                //Note that we already have firstPart declared above, with that in mind, I would suggest trying to combine that with String.EndsWith. Again, google is your friend
                //Example of what you would send in discord to call this command: "!add 5 2" and the result should be the bot sending back: "7"
                if (false) //replace false with a check that sees if the user called the add command
                {
                    try
                    {
                        //TODO see if the correct parts of the message can be converted to an integer. If so, add the two numbers together and then call ReplyToMessage and pass back the new number.
                        //if you want a hint as to how to find these two parts, look at line 17. As for converting numbers, google it, but I'll let you figure out how to word this one. Learning how to google your problems is a critical step in all programing so you HAVE to learn it, you'll never make it anywhere if you can't figure out how to express to google what your problem is. If you aren't sure what to look up, take whatever you would ask me, take out anything specific to this program (like "the item on line 39") and once you've done that put the rest of your question into google.
                        //check to see if the message can be parsed into your two ints (this does not need to be robust, if the user doesn't input it as I showed in the example above, it should just not work) (it COULD be more robust but I wouldn't worry about it for now)

                        int myIntName = 69;//Just declaring the ints for you to use. You will need to change all three of these with your code's logic.
                        int mySecondIntName = 420;
                        int myFinalIntName = 80082;

                        //again, if you are struggling with the correct syntax to use here, but you know what you want to do (add two integers for example) just google it. A good rule of thumb for google syntax related questions is to specify in the search what language you are using, in this case C#.


                        //TODO if not already done, parse the two numbers from the user's input and set myIntName and mySecondIntName to the results.
                        //TODO add the two integers and set myFinalIntName to be equal to them combined.
                        //TODO send a reply letting the user know what the two numbers added is equal to.

                        //if you are trying to figure out how to pass your ouput into ReplyToMessage, consider using this format: $"{parameterName} regular string text here {anotherParameterName} more text here" as the input. This way you could also have something like $"{myIntName} plus {mySecondIntName} is equal to {myFinalIntName}!" as your output text.
                    }
                    catch
                    {
                        Console.WriteLine($"the message could not be parsed to an int");
                    }
                }
                else if (false)//TODO use the above code as a reference. Create another action which will do the following. User input: "!subtract 3 9" Bot output "3 minus 9 equals -6!"
                {

                }
            }
            else //you can use an else satement like this to always run if the above if check failed. Conversly, if the if check succeeded then this will not run.
            {
                Console.WriteLine($"the message was not intended to be used by the bot and so it will be ignored.");
            }
        }
        static void ReplyToMessage(string replyContent)
        {
            incomingMessage.Channel.SendMessageAsync(replyContent);
        }
    }
}
