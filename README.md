
# ecosystem

This is a computer graphics university project I did. it is an implementation of an ecosystem.

The idea for the project came from this youtube video:

https://www.youtube.com/watch?v=r_It_X7v-1E&ab_channel=SebastianLague


The way it works is there are two types of animals in this ecosystem:

A wolf:

![wolf](https://user-images.githubusercontent.com/73134488/213860184-ca9ae90b-6956-4dac-af51-399b4da0a448.jpg)

And a sheep:

![sheep](https://user-images.githubusercontent.com/73134488/213860225-639b257a-3113-4ea4-9b72-9218a9eee8ab.jpg)

The wolf eats the sheep and the sheep eats this red plant:

![flower](https://user-images.githubusercontent.com/73134488/213860288-7ecccc2f-2344-400b-b58e-b95af7fc9679.jpg)

The first screen is the main menu, where the user can choose to start the simulation, go to settings or quit:

<img width="632" alt="image" src="https://user-images.githubusercontent.com/73134488/213860485-874f3982-fd18-48f9-8354-d14db13e285e.png">

In the setting menu, the user can choose the number of wolves and sheep that will appear at the start of the simulation:

![settings](https://user-images.githubusercontent.com/73134488/213860527-c8f10104-8372-45bb-9618-bbb879cda3bb.jpg)

after the simulation is starting, the wolves and sheep will wander around the scene looking for food:

[Untitled video - Screen Recording - 1_21_2023, 11_22_04 AM.webm](https://user-images.githubusercontent.com/73134488/213860728-694a697d-b82f-474a-9fcf-49de48cc845e.webm)

The user can move the camera around the scene using the keyboard arrows and the mouse scroller. 

Each animal has a hunger bar floating above it. every four seconds the hunger bar is emptied by a certain amount unless the animal found food, and then it's filled.

If two animals of the same species and different gender encounter each other, there's a certein probabilty the female will get pregnent. and after a few seconds will give birth to new animals.

Each animal (wolf and sheep) has the next traits:
* Gender - can be a male or a female
* Speed - deternmines the speed of the movement of the animal around the scene
* Mating Desire - everytime an animal sees food, there's a certein probability it will approach the food. if an animal has a high mating desire, there's a higher chance it will prefer to avoid the food and keep looking for a mate.
* Likeliness To GetSick - every five seconds, there is a "virus" spreading, and each animal has a certain probability to catch it. the Likeliness To GetSick trait determines the probability of an animal catching the virus. if the animal caught it, it will become green and its speed will decrease.

![sickness](https://user-images.githubusercontent.com/73134488/213861522-a68d9bb8-f54e-4355-92d2-8e035d30dcec.jpg)

* Immune System Probs -  if an animal caught the virus, after ten seconds (from the moment the animal caught it) a random number from 0 to 1 is generated, and if it's higher than the animal's Immune System Probs trait, the animal will die.

![likely](https://user-images.githubusercontent.com/73134488/213861565-0d30f9ce-2ff6-49c5-8682-27a0585ba2e8.jpg)

* Longevity - the amount of time an animal lives in the simulation.

* Attractivnes - when a female animal encounters another male animal of the same species, it chooses to mate with it based on the amount of attractiveness trait it has. if it's lower than the female's attractiveness by 0.12, the female will not mate with the male. 

![attractivnessCode](https://user-images.githubusercontent.com/73134488/213861456-e37116fe-ef5d-450b-a861-0ff5f2bf9ad8.jpg)

when a female animal gives birth to an off spring, its genes are created by using a weighted average betweem both of its parants genes:

<img width="569" alt="image" src="https://user-images.githubusercontent.com/73134488/213874516-84fd03fd-c91c-4333-b6b2-9d15c85eb4e7.png">


After starting the simulation, the user can decide to stop the simulation and observe some statistics about the last simulation run.

the statistics shows 2d graphs that contains the average amount of each trait as a function of time since the beginning of the simulation

![graph](https://user-images.githubusercontent.com/73134488/213873860-b025a921-60e3-4e02-9089-a7e70d181444.jpg)

![graph2](https://user-images.githubusercontent.com/73134488/213873861-22adce8e-35c0-4a06-8820-2c34890f409b.jpg)

The way it works, is that every four seconds, the data of all of the wolves and sheep is being collected and and added to lists that will presented in the statistics screen:

![stats](https://user-images.githubusercontent.com/73134488/213874004-9db7cdd1-805c-44b8-8750-edac3868333a.jpg)


