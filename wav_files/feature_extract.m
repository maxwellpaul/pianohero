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

[y,fs] = audioread('Simple_Piano.wav');
    dt = 1/fs;
    t = 0:dt:(length(y)*dt)-dt;
    %plot(t,y); 
     xlabel('Seconds'); 
     ylabel ('Amplitude');



[pks, locs] = findpeaks(y(:,2));
plot(t, y, t(locs), pks, 'or');

A = [];
B = [];
D = [];
C = [];
E = [];

for index = 1:length(pks)
    if pks(index) > .35
        A = [A,pks(index)];
        B = [B,locs(index)];
        %get the index from locs, then index into y with that
        C = [C, y(locs(index),2)];
        time = locs(index) / length(t);
        E = [E,time];
    end
end

maxElem = max(C);
minElem = min(C);
diff = maxElem - minElem;
thresholdLength = diff/4;
div1 = minElem + thresholdLength;
div2 = div1 + thresholdLength;
div3 = div2 + thresholdLength;

for index = 1:length(C)
    if C(index) >= minElem && C(index) < div1
        D = [D,1];
    elseif C(index) >= div1 && C(index) < div2
        D = [D,2];
    elseif C(index) >= div2 && C(index) < div3
        D = [D,3];
    else
        D = [D,4];
    end
end

siz = length(t) / fs;

fid=fopen('MyFile.txt','w');
for index = 1:length(D)
    fprintf(fid, '%d:%f\n', D(index), E(index) * siz);
end
fclose(fid);
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
