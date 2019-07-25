/*  Gameplay revamp

////////// trap engages player
	            timer rolls
		            roll 3-9 (placeholder) based on difficulty
		            randomly assign values between 0 and timer max
		            incrementally assign speed values (later = faster?)
		            instantiate hazard, pass speed to hazard
	            timer runs out
                tally up score
                wait a little, animate player shaking off ice
////////// trap disengages player


////////// hazard instantiates directly above player
	            max speed assigned as passed value
	            accelerate to max speed downwards
	
	            if collision with player
		            animate
		            end state
	            else if collision with ground
		            animate
		            fade and destroy

////////// player accepts trap state
	            click to dodge
	            spam dodging without shorts out player, can't dodge for x time
                
                if dodge occurs
                    measure Y distance between player's top and hazard's bottom
                    pass 
 
    Actual "To do's"
        - Change trap prefab to include snowball
        - add collision script to snowball to trigger end
        - Figure out how to handle measuring distance to a virtual object and have the focus rapidly change
        / based on the current "one to dodge"
        - work with 3 snowballs, set time intervals
            - then randomise intervals
            - then randomise number of snowballs
        - keep it modular! pass the script hazard types!
        
    
  

    Additional "Would like to do's"
        
        - Revamp code to be more modular -> more specifics in main, less dependencies in specialist scripts
        - Assets!
        - Flesh out design for additional environments and procedurally generated blocks
            - look into colour/lighting shifting 
                - underwater makes everything darker even above the waterline?
                - cave gloomier than forest
                - day/night cycle time lapse as you play a la Alto's Adventure
        - look at Move_Right() again, it feels lazy and too all-encompassing (always move when x&y not true)

    New design questions

        - Do we want the character to dodge, or defend from hazards (ie laser-destroy hazards)?
                - dodge may feel off, especially if it's 2D perspective and the character is moving
                - defending more suitable to moving character, 
                / arguably more suitable for rooted (ie Missile Defence)
                
                    - can also have a charge-up time relative to proximity of hazard
                    - can only defend self when it's close enough, 
                    / without feeling arbitrary if it were a dodge
                
                    - similar to Paper Mario TTYD example, and to Space Frontier's "Green Band"

        - How to keep the game short?
                - speed value with ever-encroaching ice on player, speed bumped up by successful dodges?
                - do we stop the player at all? or just slow down?

                    - why have traps when we can have danger "storms" and 
                    / have the speed closely tied to player feedback and
                    / not break immersion every few seconds by stopping 
                    / the character dead for no good visible reason

                    - inversely, why have the character moving at all?
                        - plodding movement feels boring
                        - rhythm-like timed taps might feel bad with a moving character, 
                        / especially for x-static falling objects like icicles or snow
                        / Missile Defense-like gameplay possible, gradually more hectic
                        / dodging until it's extremely difficult
                        / difficulty breaks to allow chance to breathe


        - Difficulty ramp up, how?
            - shorter overall timeframes
            - shorter minimum intervals allowed between hazard spawn/impacts
            - increasing hazard speeds
            - randomising hazard speeds

        - Ok-Great-Excellent player feedback design for closer dodges, rewards are?
            - extra wisps
            - more speed
            - more score (nobody cares about a score unless it's tied to currency imo)
 */


/*   Initial design notes


    <<<< Main loop >>>> - Spawn, initial settings, animation, gamestate to -beginning-
                          > Beginning - any initial setup, splash screen of instructions, small animation, etc etc
                                  - Generate first trap layers & spawn offscreen
                                  - Character move horizontally to trap position, gamestate to -moving-
                          > Moving - constant velocity right, "walk" cycle, background parallax and move around 1.5 view lengths to new trap's position
                                      - play trapping animation/sound, gamestate to -trapped- 
                          > Trapped - begin timer
                                    - accept, count & show count for discrete touch inputs
                                    - if count = layers, begin new (1 second?) timer, if more touches are detected, run fail (too many) sequence
                                                          else run success sequence
                                    - else if timer runs out, and layer count not matched, run fail (too few) sequence
                          > success sequence - break-trap animation, PC jumps up and right, when it lands constant velocity right + "walk cycle" + background parallax & move 1.5L to new trap                                  
                                   - run procedural progression (On successful trap break below)
                                   - reduce next trap start timer
                                   - Moving state
                           > Repeat per block (see below - play progression)
                           > Gravity is currently set at -15m/s/s up from default 9.81 to bandaid fix floaty jumps

    <<<< Environment Design >>>>
       > Icy surface of a lake
       > Snow on top of ice
       > Snowy mountains/terrain as background & parallax
       > 2D-ish view slice in foreground like many 2.5D games
       > Water under the snow/top ice layer
       > traps indicated by layers of ice under the player when stuck
       > Possibility of a dark monster/large shadow threat in the water following under the player?
       > Stark lighting, if not monotone - desaturated colours
       > Maybe burst of colour when you break out of a trap?

    <<<< Traps >>>>
       > Store procedural traps in list/vector
       > On successful trap break, begin on-screen events
           generate new next, spawn assets
           Assets for trap - ice layers, believable convergence/divergence (or sleight of hand eg. blizzard), snow floor, 
                               trap object (flat spot, ice shards pointing up when trapped), trap collider, trap script

    <<<< Dos >>>>
       > platform destructor - same idea as constructor - 
               >> All trap prefab objects have been tagged as Trap, iterate through gameObjects tagged for Trap, 
               >> check X coordinate and delete any with x < Admiral_Ackbar_DE
       > have no instances of platforms at the start - DONE -> Prefabs bruh
       > procedurally extend "surface" and "floor" planes
       > actually put stuff in Main_DE script! Other than comments!
                   >> (wanting triggers and inputs filtering through Main_DE for UI and any overarching mechanics)
                   >> (rather than passing direct to other objects' scripts)

   <<<< Hows? >>>>
       > properly developed gamestates
       > Water effect
       > parallax backgrounds

   <<<< Extra suggestions >>>>
       > Play progression - pauses between gameplay blocks - 
                            ie iceberg break going to next landmass, count up current points, 
                            show previous best etc idk - character taking a breather
       > something-in-the-water swirl/shadow effect during trap/constantly following under player 
                   >> to suggest the idea of Black Dog analogy for depression
       > Blizzard weather effect
       > other varying environment methods for indicating progression?
                   >> day/night, changes in weather, wind, background tiles
                   >> slopes, vertical element, other types of traps? (ice walls blocking path?)
       > timed taps gameplay like rhythmn or Space Frontier, rather than 1-5x taps quickly? 
                   >> Possibly timed visually with dark thing in water rising up through the layers at ya
                   >> Rewards careful timing rather than button mashing, which might be more frustrating to fail
                   >> Morse Code timing!


   <<<< Design Questions >>>>

       > Need a more fleshed out design for environment
                   >> happy with this viewpoint or rather go full 2D and have a flat slice?
                   >> Decided on frozen lake as setting, and layers of ice below the character as the traps?
       > Thinking more about early/mid/late game - Graeme's difficulty block pacing idea (outlined a little above)
       > mixing up gameplay/progression between sessions etc
    */
