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
t = linspace(0, length(y)/fs, length(y));
ydft = fft(y);
% I'll assume y has even length
ydft = ydft(1:length(y)/2+1);
[pks, locs] = findpeaks(abs(ydft));

plot(t, y);

% create a frequency vector
freq = 0:fs/length(y):fs/2;
% plot magnitude
%plot(freq,abs(ydft));
% plot phase
%plot(freq,unwrap(angle(ydft))); 
xlabel('Hz');
ydft_vect = abs(ydft);

%plot(freq,abs(ydft), freq(locs), pks, 'or');
