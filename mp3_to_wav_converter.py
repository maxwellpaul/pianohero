import pydub, sys, os, wave
import numpy as np
import scipy.io.wavfile
from pydub import AudioSegment
import soundfile as sf
import matplotlib.pyplot as plt
import matplotlib as mpl
from os import walk

# TO RUN: python mp3_to_wav_converter.py song_name(without.mp3 at the end)

mpl.rcParams['agg.path.chunksize'] = 10000

mypath = "PianoHeroResources/MP3Files/"

f = []
for (dirpath, dirnames, filenames) in walk(mypath):
    f.extend(filenames)
    break

for filename in f:
    if(filename[0] != '.'):
        name = filename[0:len(filename) - 4]
        song_name_mp3 = "PianoHeroResources/MP3Files/" + name + ".mp3"
        song_name_wav = "PianoHeroResources/WAVFiles/" + name + ".wav"
        
        pydub.AudioSegment.converter = r"/usr/local/Cellar/ffmpeg/3.4.1/bin/ffmpeg"
        
        song = AudioSegment.from_mp3(song_name_mp3)
        
        song.export(song_name_wav, format = "wav")
        #song.export("PianoHero/Assets/Songs/song_wav/" + str(sys.argv[1]) + ".wav", format = "wav")
        song.export("wav_files/song_queue/" + name + ".wav", format = "wav")

#song_name_mp3 = "PianoHeroResources/MP3Files/" + str(sys.argv[1]) + ".mp3"
#song_name_wav = "PianoHeroResources/WAVFiles/" + str(sys.argv[1]) + ".wav"

#pydub.AudioSegment.converter = r"/usr/local/Cellar/ffmpeg/3.4.1/bin/ffmpeg"

#song = AudioSegment.from_mp3(song_name_mp3)

#song.export(song_name_wav, format = "wav")
#song.export("PianoHero/Assets/Songs/song_wav/" + str(sys.argv[1]) + ".wav", format = "wav")
#song.export("wav_files/song_queue/" + str(sys.argv[1]) + ".wav", format = "wav")
