#!/bin/bash
cd ../../..
source 498/bin/activate
python mp3_to_wav_converter.py
deactivate
cd PianoHeroResources/MP3Files
cd ../../wav_files/
./run_feature_extract.sh /Applications/MATLAB/MATLAB_Runtime/v93
cd song_queue