import pydub, sys, os
from pydub import AudioSegment
import soundfile as sf
import matplotlib.pyplot as plt
import matplotlib as mpl

mpl.rcParams['agg.path.chunksize'] = 10000



song_name_mp3 = str(sys.argv[1]) + ".mp3"
song_name_wav = "wav_files/" + str(sys.argv[1]) + ".wav"

pydub.AudioSegment.converter = r"/usr/local/Cellar/ffmpeg/3.4.1/bin/ffmpeg"

song = AudioSegment.from_mp3(song_name_mp3)

song.export(song_name_wav, format = "wav")

sig, samplerate = sf.read(song_name_wav)

print(sig.shape)
#plt.plot(sig);
#plt.show()
