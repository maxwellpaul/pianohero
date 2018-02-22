PLEASE READ ENTIRE READ ME BEFORE USE

-----------------------------------APPLICATION DEMO INSTRUCTIONS--------------------------------------------------

BACKEND FILES ARE NOT YET LINKED TO THE APPLICATION AND THE UPLOAD FEATURE ON THE DEMO APP WILL NOT FUNCTION. END OF README EXPLAINS EACH OF THE BACKEND FILES USED TO GENERATE SONGS AND HOW TO RUN THEM WITH THE DEMO MP3 FILES FOR YOU TO TRY


Steps on how to play Piano Hero DEMO APPLICATION:

As you successfully hit notes, the rock meter needle will move to the right. If you miss a note the rock meter needle will move to the left. If you miss enough notes, and the rock meter reaches all the way to the left, the game will end early

1. Pull from Git Repo
2. Start up the application (PianoHeroApp) by double clicking on it
3. Click on settings tab and set the resources folder path to be "PianoHeroResources/" and then click set Path (nothing will change but things will have happened)
4. Click back to main menu
5. Click select a song and choose start me up. (Simple piano will not work, we pre loaded the application with start me up)(the notes generated for this song were generated using our algorithm)
6. Once song has been selected hit play!
7. The controls for playing the notes are a,s,d,f on the keyboard
8. When the song is over, you can choose to play the song again or go back to the main menu and choose a new song


--------------------------------------------------BACKEND--------------------------------------------------
mp3_to_wav_converter.py:

Summary - Most songs are downloaded as Mp3 files. Unfortunately, it is very difficult to extract certain features of audio files in this format. In order to reconsile this we have written a script in python which will automatically convert uploaded Mp3 files (to the applitcation) into WAV file format. To run this file follow these steps:
1. Open terminal and navigate to the directory you pulled the git repo into.
2. Launch the virtual environment we have constructed by calling source 498/bin/activate in terminal.
3. Install the required libraries by running pip install -r requirements.txt (this should download all required libraries to run the algorithm)
4. Run the script by calling python mp3_to_wav_converter.py 'songname.' We have supplied you with start_me_up.mp3 so to run it just call python mp3_to_wav_converter.py start_me_up. This will create a start_me_up.wav file in the directory wav_files and in the PianoHero Unity assets. 


feature_extract.m:

Summary - This is where the meat of our project is. We have written an algorithm which analyzes running averages of amplitudes of pitch of wav files and based on moving thresholds, converts a segment of the wav file to one of four possible notes. The output of this is a txt file where each line represent a note (0-3) and a timestamp (where exactly in the song the note corresponds to)To run this file follow these steps: (you need matlab)
1. Navigate to the wav_files directory and open the directory in Matlab
2. You can now run the script on any wav file in the directory provided but for this example lets use the wav file you just generated using mp3_to_wav_converter.py. To do this go to the command windown and type in 'feature_extract('start_me_up')'
3. This will automatically generate the txt file I mentioned earlier and place it in the PianoHero/Assets/Songs directory named start_me_up.txt. It will also generate a graph of all the running averages we calculated for that specific song (there should be 60 total) so you can see exactly how the algorithm is analyzing the wav file.
4. This text file can now be used by the unity application (Not for Alpha, but once we link the two it will)

--------------------------------------------------PIPELINE FOR BETA--------------------------------------------------
For the Beta release we hope to add the upload mp3 feature shown in the demo application which you sampled in the beggining of this tutorial. It will work by selecting a file, automatically calling mp3_to_wav_converter.py and converting the mp3 to a wav file. Then it will automatically run the matlab script feature_extract.m to generate the txt file. The application will then update its settings automatically to add the uploaded song to its list of playable tracks.

--------------------------------------------------CONTACT INFORMATION-----------------------------------------------
If you have any questions on how to run the application or how the algorithms work please feel free to email the Piano Hero team at any of:
averubin@umich.edu,
paulmax@umich.edu,
jtknox@umich.edu,
czechben@umich.edu
