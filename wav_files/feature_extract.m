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

%put first index in
%push next one that is over .5


for index = 1:length(pks)
    if pks(index) > .15
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

F = sort(C);
fSize = length(F);
fSize = ceil(fSize / 4);

div1 = F(fSize);
div2 = F(2*fSize);
div3 = F(3*fSize);

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

G = []
H = []
G = [G,E(1)];
H = [H,D(1)];
for index = 2:length(E)
    if (E(index)*siz) - (G(length(G))*siz) >= .25
        G = [G,E(index)];
        H = [H,D(index)];
    end
end

fid=fopen('MyFile.txt','w');
for index = 1:length(G)
    fprintf(fid, '%d:%f\n', H(index), G(index) * siz);
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
