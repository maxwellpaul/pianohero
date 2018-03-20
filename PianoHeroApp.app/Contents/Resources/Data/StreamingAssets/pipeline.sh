#!/bin/bash
source /Users/paulmaxwell/Desktop/EECS\ 498/PianoHero/pianohero/498/bin/activate
python /Users/paulmaxwell/Desktop/EECS\ 498/PianoHero/pianohero/mp3_to_wav_converter.py
deactivate
cd PianoHeroResources/MP3Files
cd ../../wav_files/
./run_feature_extract.sh /Applications/MATLAB/MATLAB_Runtime/v93
cd song_queue
