# Economy Test

Basic Economy Simulation with a small Ascii Map to go along with it.


## Download

### [GitHub Releases](https://github.com/AlbinoGeek/Economy/releases)  
Latest: v0.1.0-alpha


## Screenshots

Console generation and display of a [`Map`](/EconomyTest/Map.cs), and round / turn-based simulation of a [`Market`](/EconomyTest/Economy/Market.cs) full of [`Agent`](/EconomyTest/Economy/Agent.cs)s

![economy simulation](http://i.imgur.com/J2Q42C0.png)

Each Turn, `Agent`s must consume some kind of `Food` (`Bread` counts) and some kind of `Water` (`Liquor` counts) to survive.  When only one `Agent` is left surviving, they are marked as the winner.

![economy winner](http://i.imgur.com/IvnmR4E.png)


## TODO

- Fix Exception on case: No Winner (last two people die simultaniously)
- `Map` should generate some items by defualt
- `Agent`s should move around the `Map`
- `Agent`s should be able to `Trade` with each-other if near each other
- `Agent`s should be able to search for items on the `Map`
