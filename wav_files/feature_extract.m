%[y, fs] = audioread('Simple_Piano.wav');
%t = linspace(0, length(y)/fs, length(y));
%plot(t, y);
%Nfft = 1024;
%Nfft = 2^nextpow2(length(y));
%f = linspace(0, fs, Nfft);
%G = abs(fft(y, Nfft));
%figure; plot(f(1:Nfft/2), G(1:Nfft/2));

%G_vec = linspace(0, length(G)/fs, length(G));
%[pks, locs] = findpeaks(G_vec);
%print(pks);
%figure; plot(f(1:Nfft/2), G(1:Nfft/2), f(locs), pks, 'or');
%print(pks)
%plot(pks, locs);

%Change to whatever wav you want
%reads in frequencies and converts to amplitude

[y,fs] = audioread(strcat(songName, '.wav'));
dt = 1/fs;
t = 0:dt:(length(y)*dt)-dt;
%plot(t,y); 
xlabel('Seconds'); 
ylabel ('Amplitude');


%finds peaks of amplitude
[pks, locs] = findpeaks(y(:,2));
plot(t, y, t(locs), pks, 'or');

peakValues = [];
peakFreqs = [];
timeRatios = [];


%put first index in
%push next one that is over .5

%stores values of peaks and their indices that are above a certain threshold
%Will have to find a way to programatically calculate this threshold
for index = 1:length(pks)
    %if peak is above this threshold, store it and its index
    if pks(index) > .25
        peakValues = [peakValues, pks(index)];
        %Grab the corresponding frequency for the peak
        peakFreqs = [peakFreqs, y(locs(index),2)];
        %Take the index of the peak and divide it by the total num of
        %indices to find approx location in song
        timeRatios = [timeRatios, locs(index) / length(t)];
    end
end

%Find the max and min frequencies out of the filtered peaks
maxElem = max(peakFreqs);
minElem = min(peakFreqs);

%find the number of filtered frequencies so you can divide them into 4 sections
sortedFreqs = sort(peakFreqs);
freqsSize = length(sortedFreqs);
freqsSize = ceil(freqsSize / 4);

%Indices to divide notes into 4 sections
divider1 = sortedFreqs(freqsSize);
divider2 = sortedFreqs(2*freqsSize);
divider3 = sortedFreqs(3*freqsSize);

%Assign each peak a note based on the frequency
notes = zeros(1,length(peakFreqs));
for index = 1:length(peakFreqs)
    if peakFreqs(index) >= minElem && peakFreqs(index) < divider1
        notes(index) = 1;
    elseif peakFreqs(index) >= divider1 && peakFreqs(index) < divider2
        notes(index) = 2;
    elseif peakFreqs(index) >= divider2 && peakFreqs(index) < divider3
        notes(index) = 3;
    else
        notes(index) = 4;
    end
end

%this is the length of each snapshot
snapShotSize = length(t) / fs;

%Filter all notes by how far away in seconds they are from each other
%Right now I have it so that if a note is less than .25 seconds away from
%the last note you don't keep it
filteredNoteTimes = [];
filteredNotes = [];
%Initialize with the first note
filteredNoteTimes = [filteredNoteTimes,timeRatios(1)];
filteredNotes = [filteredNotes,notes(1)];
for index = 2:length(timeRatios)
    %If the notes are more than .25 second apart keep them
    if (timeRatios(index)*snapShotSize) - (filteredNoteTimes(length(filteredNoteTimes))*snapShotSize) >= .25
        filteredNoteTimes = [filteredNoteTimes,timeRatios(index)];
        filteredNotes = [filteredNotes,notes(index)];
    end
end

outFileName = strcat(songName,'.txt');

%Open a file to write the notes to
fid=fopen(outFileName,'w');
for index = 1:length(filteredNoteTimes)
    fprintf(fid, '%d:%f\n', filteredNotes(index), filteredNoteTimes(index) * snapShotSize);
end
fclose(fid);

movefile(outFileName, '../PianoHero/Assets/Songs');
% code

%[y2,fs2] = audioread('Simple_Piano.wav');
%y2 = y2(:, 2);
%spectrogram(y2, 256, [], [], fs2, 'yaxis');

% create a frequency vector
%freq = 0:fs/length(y):fs/2;
% plot magnitude
%plot(freq,abs(ydft=));
% plot phase
%plot(freq,unwrap(angle(ydft))); 
%xlabel('Hz');
%ydft_vect = abs(ydft);

%plot(freq,abs(ydft), freq(locs), pks, 'or');
