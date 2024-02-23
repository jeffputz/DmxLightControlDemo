# Dmx LightControl Demo
### Used for 2024 Orlando Code Camp talk

Most of the time, writing software means moving stuff around in memory, or maybe on a screen. Other times, we get the opportunity to move physical things in the real world, and that's more fun.

Lighting control software is very expensive, but it the underlying protocols are actually relatively simple. In this demo, we'll break down the DMX protocol, as well as a couple of the higher level protocols, including sACN and Art-Net.

__Note:__ This demo is written in C#, but the concepts are applicable to any language.

[Link to deck on Google Slides](https://docs.google.com/presentation/d/1PpcDSFAqYPUxOblurKsxu1_OHazeDANY7mnKOMI5hNc/edit?usp=sharing)

[Link to YouTube video](https://www.youtube.com/watch?v=LB0YL_kyzKw)

## Running the project
This code uses .Net and C#, so it should run just fine on any platform, using Jetbrains Rider or Visual Studio. It's using server-side Blazor, which is stateful and that's useful in this case (I wouldn't use it for a real web app though, rather Blazor WASM and an good ol' API instead). The `appsettings.json` config file requires that you put your local IP address in there, so the software knows which network interface to bind to. Where you find it depends on your OS.

The demo is outputting control via sACN, so you'll need an sACN interface on your network, and the lights connected to it via DMX cables. Configuring the interface is up to you, but I used a Chauvet DMX-AN2 for the demo, and set the first universe on it to 1, updating at 40Hz. I also gave it a static IP so I could reliably login to its administrative interface. It's a good interface to use, because it has a blinky light when it's receiving DMX.

The `Orchestrator` class defines two lighting fixtures, which in this case are Chauvet Intimidator Spot 260x's, but you can define anything you'd like with your own fixtures.

For the specifics, check the deck and the video!

## Other notes
There's a build here of `Kadmium-sACN` included in the `lib` folder, which is a library used for interfacing with sACN. The project appeared to be abandoned when I found it, and it had a bug that I submitted a PR for. My fork is here: [jeffputz/kadmium-sacn](https://github.com/jeffputz/kadmium-sacn)
