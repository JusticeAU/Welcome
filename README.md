# RPG
 'Welcome' is a short text based adventure written in C#.

'Welcome' actually started as StringTheory, but I found it difficult to write a Class Library without really knowing how I would to use it in a game.

My programming class teacher had issued a small challange to us, to write a turn-based battle system in C# - So I thought it a good opportuinity to:

1. Have something that utilises the library as im writing it
2. Make a 'complete' game.

I wrote the battle system, the wrapped it around StringTheorys sequences.

I had to create a Story Manager to deliver the dialogue. If I had more time, I would have all this story data as exactly that, data, in text files and have the program load it in.

It was fun writing the story at a high level, implementing each beat, and programming it all to link together.

The compiled game can be downloaded from https://www.justinetchell.com/games/welcome

To compile from source you will also need to download StringTheory source and have the below directory structure:

../
	RPG/
		RPG.sln
	StringTheory/
		StringTheory.sln
		
and build from RPG.sln