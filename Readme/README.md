# Voice Activated Control for Firing a gun in 2d game.

### Configuration needed:
1. Unity 4.3 or higher
2. Windows.


### About the game and Voice activated control

We have a simple 2d game in which our protagonist “Mr. Beans” is supposed to dodge the enemies (using arrow keys) and shoot them using voice activated command “shoot”.
So whenever player says “shoot” in the microphone the bazooka which Mr. Beans is carrying launches the missile which helps in killing the enemies.


### How to step up the Game

1.	Open Readme Folder in the project and double click on “RecoServeurX64.exe” .
{if your windows is 32 bit then click open the folder “if windows is 32 bit” inside the readme folder. Then double click on “RecoServeurX86.exe”}
2.	Open the project in unity and hit the play button.
3.	Say “shoot” in the microphone and you will see that Mr. Beans launches missiles on your voice Command.
4.	You can use arrow keys to move around in the scene.


### Technique used for Voice Activated control

I am using Windows UDP server for voice recognition. It parses the voice command and tries to match them with the list of words in ‘grammar.txt’ (inside readme folder). For the current program I have validation recognition of 70%  . It means that if the voice command and the actual word in the text file matches by 70 % or greater then only proceed further. Else discard the command and be ready to listen the next one. In my research I found that 70 % is a fairly good number. Neither too strict nor too soft. 






