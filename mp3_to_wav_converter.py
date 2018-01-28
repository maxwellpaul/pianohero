from pydub import AudioSegment

song = AudioSegment.from_mp3("test.mp3")

song.export("test.wav", format = "wav")


