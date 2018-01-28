import pydub, sys, os
from pydub import AudioSegment
import soundfile as sf
import matplotlib.pyplot as plt
import matplotlib as mpl
from pyAudioAnalysis import audioBasicIO
from pyAudioAnalysis import audioFeatureExtraction

# TO RUN: python mp3_to_wav_converter.py song_name(without.mp3 at the end)

mpl.rcParams['agg.path.chunksize'] = 10000

song_name_mp3 = str(sys.argv[1]) + ".mp3"
song_name_wav = "wav_files/" + str(sys.argv[1]) + ".wav"

pydub.AudioSegment.converter = r"/usr/local/Cellar/ffmpeg/3.4.1/bin/ffmpeg"

song = AudioSegment.from_mp3(song_name_mp3)

song.export(song_name_wav, format = "wav")

[Fs, x] = audioBasicIO.readAudioFile(song_name_wav);
F = audioFeatureExtraction.stFeatureExtraction(x, Fs, 0.050*Fs, 0.025*Fs);
plt.subplot(2,1,1); plt.plot(F[0,:]); plt.xlabel('Frame no'); plt.ylabel('ZCR'); 
plt.subplot(2,1,2); plt.plot(F[1,:]); plt.xlabel('Frame no'); plt.ylabel('Energy'); plt.show()


#sig, samplerate = sf.read(song_name_wav)

#print(sig.shape)
#plt.plot(sig);
#plt.show()
