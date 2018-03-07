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

    %Length of the song in seconds
    songLengthSecs = length(t) / fs;

    %finds peaks of amplitude
    [pks, locs] = findpeaks(y(:,2));
    
    %The vector of amplitude data we are actually using from y
    real_y = y(:,2);

    % How many frames are in a second
    second_frame_size = ceil(length(real_y)/songLengthSecs);

    %Number of frames in a window of 5 seconds
    framesPerWindow = 5 * second_frame_size;
    
    %Array that holds the data of the current window you are looking at
    window = zeros(1,(framesPerWindow));
    
    %The number of ~5 second windows in the song
    numWindows = ceil(length(real_y)/(framesPerWindow));
    
    %Array that will hold the average amplitudes for each window
    averages = zeros(1,numWindows);
    
    average_index = 1;
    window_index = 1;

    %SUMMARY:This for loop is grabbing the average amplitude value for data
    %above our minimum threshold, for every single window of length 5
    %seconds in our song
    for index = 1:length(real_y)
        %If the amplitude exceeds our bare minimum of .05
        if(real_y(index) > .05)
            %add that val to the current windows data
            window(window_index) = real_y(index);
        end
        window_index = window_index + 1;
        %If we have passed over a whole window
        if (mod(index, (framesPerWindow)) == 0)
            %get rid of all excess 0's
            window = window(window ~= 0);
            %Grabs the average amplitude of that window
            averages(average_index) = mean(window);
            average_index = average_index + 1;
            %reset the window
            window = zeros(1,(framesPerWindow));
            window_index = 1;
        end
    end
    
    %Used for plotting the averages
    x_var = (1:1:numWindows);
    plot(x_var, averages);

    %average_amps = mean(window);

    %plot(t, y, t(locs), pks, 'or');

    %Initializing relevant arrays
    peakValues = zeros(1,length(real_y));
    peakLocs = zeros(1,length(real_y));
    timeRatios = zeros(1,length(real_y));

    %Obtaining the notes(amplitudes and timeRatios) as well as the note
    %value dividers for each window
    %TODO: Need to test for unlikely case where the number of frames in the
    %song wraps exactly around the modulus. I believe this will call and
    %index out of range error
    av_index = 1;
    current_average = averages(av_index);
    current_pk_index = 1;
    currentWindow = zeros(1,framesPerWindow);
    divider_index = 1;
    dividers = zeros(1,3*numWindows);
    for index = 1:length(real_y)
        if (mod(index, (framesPerWindow)) == 0)
            av_index = av_index + 1;
            current_average = averages(av_index);
            currentWindow = currentWindow(currentWindow ~= 0);
            currentWindow = sort(currentWindow);
            totalSize = ceil(length(currentWindow) / 4);
            
            %if there are less than 4 peaks in the window, can't really
            %index into it because you will go off of the edge so just
            %going to use 0 as the divider for all dividers. Can prob find
            %a better method for this later
            if length(currentWindow) < 4
                if(isempty(currentWindow))
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                else
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                end 
            else
                dividers(divider_index) = currentWindow(totalSize);
                divider_index = divider_index + 1;
                dividers(divider_index) = currentWindow(totalSize*2);
                divider_index = divider_index + 1;
                dividers(divider_index) = currentWindow(totalSize*3);
                divider_index = divider_index + 1;
            end 
        end

        %if peak is above this threshold, store it and its index
        if locs(current_pk_index) == index && pks(current_pk_index) > current_average
            %We are basically saving the notes amplitude and its place in the song
            %here
            peakValues(index) = pks(current_pk_index);
            peakLocs(index) = locs(current_pk_index);
            %TODO: Check if the plus 1 here is ok
            currentWindow(mod(index, (framesPerWindow))+1) = pks(current_pk_index);
            %Take the index of the peak and divide it by the total num of
            %indices to find approx location in song
            timeRatios(index) = locs(current_pk_index) / length(t);
            current_pk_index = current_pk_index + 1;
        end 
        if locs(current_pk_index) == index && pks(current_pk_index) <= current_average
            current_pk_index = current_pk_index + 1;
        end
        %Run this if there are no more pks, basically skips going
        %everything else and grabs the last set of dividers. However if the
        %last pk index falls exactly on the mod, this might be called an
        %extra time which we dont want because it will probably exceed the
        %array indices
        if current_pk_index > length(locs)
            currentWindow = currentWindow(currentWindow ~= 0);
            currentWindow = sort(currentWindow);
            totalSize = ceil(length(currentWindow) / 4);
            
            if length(currentWindow) < 4
                if(isempty(currentWindow))
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                    dividers(divider_index) = 0;
                    divider_index = divider_index + 1;
                else
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                    dividers(divider_index) = currentWindow(1);
                    divider_index = divider_index + 1;
                end 
            else
                dividers(divider_index) = currentWindow(totalSize);
                divider_index = divider_index + 1;
                dividers(divider_index) = currentWindow(totalSize*2);
                divider_index = divider_index + 1;
                dividers(divider_index) = currentWindow(totalSize*3);
                divider_index = divider_index + 1;
            end 
            break;
        end
    end
    
    %Should have all of our note amplitudes and where they are in the song
    peakValues = peakValues(peakValues ~= 0);
    peakLocs = peakLocs(peakLocs ~= 0);
    timeRatios = timeRatios(timeRatios ~= 0);
    %initialize to 0's

    %Assign each peak a note based on the frequency
    notes = zeros(1,length(peakValues));
    current_divider = 1;
    currentModIndex = framesPerWindow; 
    
    for index = 1:length(peakValues)
        if peakValues(index) < dividers(current_divider)
            notes(index) = 1;
        elseif peakValues(index) >= dividers(current_divider) && peakValues(index) < dividers(current_divider+1)
            notes(index) = 2;
        elseif peakValues(index) >= dividers(current_divider+1) && peakValues(index) < dividers(current_divider+2)
            notes(index) = 3;
        else
            notes(index) = 4;
        end
        if (peakLocs(index) > currentModIndex)
            while(peakLocs(index) > currentModIndex)
                current_divider = current_divider + 3;
                currentModIndex = currentModIndex + framesPerWindow;
            end
        end
    end

    %Filter all notes by how far away in seconds they are from each other
    %Right now I have it so that if a note is less than .25 seconds away from
    %the last note you don't keep it
    filteredEasyNoteTimes = [];
    filteredEasyNotes = [];
    %Initialize with the first note
    filteredEasyNoteTimes = [filteredEasyNoteTimes,timeRatios(1)];
    filteredEasyNotes = [filteredEasyNotes,notes(1)];
    for index = 2:length(timeRatios)
    %If the notes are more than .25 second apart keep them
    if (timeRatios(index)*songLengthSecs) - (filteredEasyNoteTimes(length(filteredEasyNoteTimes))*songLengthSecs) >= .7
        filteredEasyNoteTimes = [filteredEasyNoteTimes,timeRatios(index)];
        filteredEasyNotes = [filteredEasyNotes,notes(index)];
    end
    end
    
    easySongName = strcat(songName, '_EASY');
    easyOutFileName = strcat(easySongName,'.txt');
    
    %MEDIUM
    %Filter all notes by how far away in seconds they are from each other
    %Right now I have it so that if a note is less than .25 seconds away from
    %the last note you don't keep it
    filteredMediumNoteTimes = [];
    filteredMediumNotes = [];
    %Initialize with the first note
    filteredMediumNoteTimes = [filteredMediumNoteTimes,timeRatios(1)];
    filteredMediumNotes = [filteredMediumNotes,notes(1)];
    for index = 2:length(timeRatios)
    %If the notes are more than .25 second apart keep them
    if (timeRatios(index)*songLengthSecs) - (filteredMediumNoteTimes(length(filteredMediumNoteTimes))*songLengthSecs) >= .55
        filteredMediumNoteTimes = [filteredMediumNoteTimes,timeRatios(index)];
        filteredMediumNotes = [filteredMediumNotes,notes(index)];
    end
    end
    
    mediumSongName = strcat(songName, '_MEDI');
    mediumOutFileName = strcat(mediumSongName,'.txt');
    
    %HARD
    %Filter all notes by how far away in seconds they are from each other
    %Right now I have it so that if a note is less than .25 seconds away from
    %the last note you don't keep it
    filteredHardNoteTimes = [];
    filteredHardNotes = [];
    %Initialize with the first note
    filteredHardNoteTimes = [filteredHardNoteTimes,timeRatios(1)];
    filteredHardNotes = [filteredHardNotes,notes(1)];
    for index = 2:length(timeRatios)
    %If the notes are more than .25 second apart keep them
    if (timeRatios(index)*songLengthSecs) - (filteredHardNoteTimes(length(filteredHardNoteTimes))*songLengthSecs) >= .4
        filteredHardNoteTimes = [filteredHardNoteTimes,timeRatios(index)];
        filteredHardNotes = [filteredHardNotes,notes(index)];
    end
    end
    
    hardSongName = strcat(songName, '_HARD');
    hardOutFileName = strcat(hardSongName,'.txt');
    
    %EXPERT
    %Filter all notes by how far away in seconds they are from each other
    %Right now I have it so that if a note is less than .25 seconds away from
    %the last note you don't keep it
    filteredExpertNoteTimes = [];
    filteredExpertNotes = [];
    %Initialize with the first note
    filteredExpertNoteTimes = [filteredExpertNoteTimes,timeRatios(1)];
    filteredExpertNotes = [filteredExpertNotes,notes(1)];
    for index = 2:length(timeRatios)
    %If the notes are more than .25 second apart keep them
    if (timeRatios(index)*songLengthSecs) - (filteredExpertNoteTimes(length(filteredExpertNoteTimes))*songLengthSecs) >= .25
        filteredExpertNoteTimes = [filteredExpertNoteTimes,timeRatios(index)];
        filteredExpertNotes = [filteredExpertNotes,notes(index)];
    end
    end
    
    expertSongName = strcat(songName, '_EXPE');
    expertOutFileName = strcat(expertSongName,'.txt');
    
    %Open a file to write the notes to
    fid=fopen(easyOutFileName,'w');
    fprintf(fid, '%f\n', .7);
    for index = 1:length(filteredEasyNoteTimes)
    fprintf(fid, '%d:%f\n', filteredEasyNotes(index), filteredEasyNoteTimes(index) * songLengthSecs);
    end
    fclose(fid);

    movefile(easyOutFileName, '../PianoHeroResources/NoteFiles/');
    
    %Open a file to write the notes to
    fid=fopen(mediumOutFileName,'w');
    fprintf(fid, '%f\n', .55);
    for index = 1:length(filteredMediumNoteTimes)
    fprintf(fid, '%d:%f\n', filteredMediumNotes(index), filteredMediumNoteTimes(index) * songLengthSecs);
    end
    fclose(fid);

    movefile(mediumOutFileName, '../PianoHeroResources/NoteFiles/');
    
    %Open a file to write the notes to
    fid=fopen(hardOutFileName,'w');
    fprintf(fid, '%f\n', .4);
    for index = 1:length(filteredHardNoteTimes)
    fprintf(fid, '%d:%f\n', filteredHardNotes(index), filteredHardNoteTimes(index) * songLengthSecs);
    end
    fclose(fid);

    movefile(hardOutFileName, '../PianoHeroResources/NoteFiles/');
    
    %Open a file to write the notes to
    fid=fopen(expertOutFileName,'w');
    fprintf(fid, '%f\n', .25);
    for index = 1:length(filteredExpertNoteTimes)
    fprintf(fid, '%d:%f\n', filteredExpertNotes(index), filteredExpertNoteTimes(index) * songLengthSecs);
    end
    fclose(fid);

    movefile(expertOutFileName, '../PianoHeroResources/NoteFiles/');
    
end
