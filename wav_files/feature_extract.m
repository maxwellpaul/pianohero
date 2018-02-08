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
function r = feature_extract(songName)

    [y,fs] = audioread(strcat(songName, '.wav'));
    %[y,fs] = audioread('beet.wav');
    dt = 1/fs;
    t = 0:dt:(length(y)*dt)-dt;
    %plot(t,y); 
    xlabel('Seconds'); 
    ylabel ('Amplitude');

    %this is the length of each snapshot
    snapShotSize = length(t) / fs;

    %finds peaks of amplitude
    [pks, locs] = findpeaks(y(:,2));
    real_y = y(:,2);

    % How many y values represent a second
    second_frame_size = ceil(length(real_y)/snapShotSize);



    total = zeros(1,(5 * second_frame_size));
    size_y = ceil(length(real_y)/(5 * second_frame_size));
    averages = zeros(size_y);
    average_index = 1;
    total_index = 1;

    for index = 1:length(real_y)
        if(real_y(index) > .03)
            total(total_index) = real_y(index);
        end
        total_index = total_index + 1;
        if (mod(index, (5 * second_frame_size)) == 0)
            total = total(total ~= 0);
            averages(average_index) = mean(total);
            average_index = average_index + 1;
            total = zeros(1,(5 * second_frame_size));
            total_index = 1;
        end
    end
    x_var = (1:1:size_y);
    plot(x_var, averages);

    %average_amps = mean(total);

    %plot(t, y, t(locs), pks, 'or');

    peakValues = zeros(1,length(pks));
    peakFreqs = zeros(1,length(pks));
    timeRatios = zeros(1,length(pks));


    %put first index in
    %push next one that is over .5

    %stores values of peaks and their indices that are above a certain threshold
    %Will have to find a way to programatically calculate this threshold
    av_index = 2;
    current_average = averages(1);
    note_divider_indices = zeros(1, length(averages));
    note_divider_index = 1; 
    for index = 1:length(pks)
        % adjusts the current average for time of song
        if (mod(index, (5 * second_frame_size)) == 0)
            note_divider_indices(note_divider_index) = index;
            note_divider_index = note_divider_index + 1;
            current_average = averages(av_index);
            av_index = av_index + 1;
        end

        %if peak is above this threshold, store it and its index
        if pks(index) > current_average
            peakValues(index) = pks(index);
            %Grab the corresponding frequency for the peak
            peakFreqs(index) = y(locs(index),2);
            %Take the index of the peak and divide it by the total num of
            %indices to find approx location in song
            timeRatios(index) = locs(index) / length(t);
        end

    end
    
    
    dividers = zeros(1, length(averages)*3);
    divider_index = 1;
    current_section = [];
    for index = 1:length(peakFreqs)
        if(peakFreqs(index) ~= 0)
            current_section = [current_section, peakFreqs(index)];
        end
        if(mod(index, size_y) == 0)
            if(length(current_section) ~=0)
                current_section = sort(current_section);
                totalSize = ceil(length(current_section) / 4);
                dividers(divider_index) = current_section(totalSize);
                divider_index = divider_index + 1;
                dividers(divider_index) = current_section(totalSize*2);
                divider_index = divider_index + 1;
                dividers(divider_index) = current_section(totalSize*3);
                divider_index = divider_index + 1;
                current_section = [];
            end
        end
    end

    timeRatios = timeRatios(timeRatios ~= 0);
    for_size = peakFreqs(peakFreqs ~= 0);
    %initialize to 0's

    %Assign each peak a note based on the frequency
    notes = zeros(1,length(for_size));
    current_divider = 1;
    note_divider_index = 1; 
    
    for index = 1:length(peakFreqs)
        if (peakFreqs(index) ~= 0)
            if peakFreqs(index) < dividers(current_divider)
                notes(index) = 1;
            elseif peakFreqs(index) >= dividers(current_divider) && peakFreqs(index) < dividers(current_divider+1)
                notes(index) = 2;
            elseif peakFreqs(index) >= dividers(current_divider+1) && peakFreqs(index) < dividers(current_divider+2)
                notes(index) = 3;
            else
                notes(index) = 4;
            end
            if (index == note_divider_indices(note_divider_index))
                current_divider = current_divider + 3;
                note_divider_index = note_divider_index + 1;
            end
        end
    end

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
    
end

% %%%%%%%%%%%%%%%%%%%%%%%%%%%%

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
