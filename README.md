# Economy Test

Created during a [livecoding.tv/angryalbino](https://www.livecoding.tv/angryalbino/) Stream.  Watch me work on it every day at 11:00 AM PST.

Basic Economy Simulation with a small Ascii Map to go along with it.


## Download

### [GitHub Releases](https://github.com/AlbinoGeek/Economy/releases)  
Latest: v0.1.3-alpha


## Screenshots

Console generation and display of a [`Map`](/EconomyTest/Map.cs), and round / turn-based simulation of a [`Market`](/EconomyTest/Economy/Market.cs) full of [`Agent`](/EconomyTest/Economy/Agent.cs)s

![economy simulation](http://i.imgur.com/9nmQBsK.png)

Each Turn, `Agent`s must consume some kind of `Food` (`Bread` counts) and some kind of `Water` (`Liquor` counts) to survive.

Starting with v0.1.2-alpha , `Agent`s will move around the `Map`.

Starting with v0.1.3-alpha , `Agent`s will Trade with each-other, and Loot dead `Agent`s.

![trade, loot, or die](http://i.imgur.com/VRWAkU1.png)

When only one `Agent` is left surviving, they are marked as the winner.

![economy winner](http://i.imgur.com/f3dQT94.png)

Dead `Agent` are shown as a `-`, where as Alive ones are shown as a `+`.


## TODO

- `Map` should generate some items by defualt
