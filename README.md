#Tennis Scrorer

#Classes:
TennisPlayer Class: Represents a player with attributes like name and current score.
TennisGame Class: Manages the scoring within a game, formats the current score, tracks points, determines the winner of the game, and resets the game.
TennisSet Class: Handles the scoring within a set, keeps track of games won by each player, and determines the winner of the set.
TennisMatchScoreList Class: Tracks the sets that have been won, keeps track of sets won by each player, and determines the match winner.

#Inheritance:
Inheritance can be useful for organising classes hierarchically.

The TennisGame class inherits from the Set class, and the Set class inherits from the MatchScoreList class.
Due to the UI not updating in Blazor, dependency injection was used to make the TennisSet class and TennisMatchScoreList class available in TennisGame.

#Interfaces:
Interfaces can be useful for defining a contract that classes must adhere to. It can be useful in this case if a different type of game has to be implemented per client requirements. E.g., doubles tennis has a different scoring system.

IGame Interface: defines the methods that a game class must implement.
ISet Interface: Defines the methods that a set class must implement.
IMatchScoreList Interface: Defines the methods that a match score list class must implement.
IPlayer Interface: Defines the methods that a player class must implement.

#Events:
Events can be useful for notifying other classes when something has happened. It's used in this case to notify the TennisGame class that the entre game has been won from the TennisMatchScoreList class.

#Enumns:
Enums can be useful for defining a set of named constants that can be changed without affecting the rest of the code. It's used in this case to define the text scores in a game.

#Exception Handling:
A try-catch block is used to handle exceptions that may occur during the game when a point is added to a player. If an exception happens, an error message is displayed to the user, and the previous state is restored to prevent any illegal states.
