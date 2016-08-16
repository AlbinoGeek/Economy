# Economy Test

Basic Economy Simulation with a small Ascii Map to go along with it.

## Created during a [livecoding.tv/angryalbino](https://www.livecoding.tv/angryalbino/) Stream.
Watch me work on it every day at 11:00 AM PST.


## Download

### [GitHub Releases](https://github.com/AlbinoGeek/Economy/releases)  
Latest: v0.1.3-alpha


## Contributing

I would love to have other people providing both input and code.  You can either:

- Create Tickets on the "Issues" Tracker
- Open a Pull Request and I will merge it
- Ask to be a Contributor and I'll add you


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


## Special `Agent`s
    
    E.B. | The Middle-Man
    - Allows two Agent to trade, at edge of radius
      - Gains +1 Money from Trades these trades```
    
    Malscythe | Item: Liquor
    - ?
    
    SadCloud123 | Item: Crystals
    - ?
    
    vassvik | Item: Meme
    - Owner of the world's supply of memes
    
    Westermin | Item: Magic Mushroom
    - ?
    
    wubbalubbadubdub | ?
    - 


## TODO

- `Map` should generate some items by defualt
- `Map` should use some ascii / colors to show different tiles
- `MapTile` with properties such as size other than `1x1`
- `MapObject` to support sizes larger than `1x1`

- `Map` Features (like River generation, water body generation)

- Special `Agent` implementation (abilities + items)

- More Items
- More Map Tiles
