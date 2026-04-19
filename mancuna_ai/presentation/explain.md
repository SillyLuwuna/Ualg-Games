# Introduction
- What i'm gonna be presenting about
- Start with a joke (subway surfers)
- Why it's important (C/assembly/digital systems example)

# The theoretical basis for data communication
## Fourier Analysis
- explain that communication can be used for any types of wave (sound, electric, electromagnetic)
- ask how we can store waves for communication
- derive fourier series
- explain how waves are represented mathematically # TODO
- the base formula on the slide is how it is stored on a computer and then sent
- the other formulas is how we can read the signal to store it

## Bandwidth-limited signals
- explain analog bandwidth (graphic explaining cutoff) (cutoff can be
artificial but usually is due to physical limitations)
- explain digital bandwidth
- explain cutoff (usually point at which power is reduced by half)
- explain baseband (0 to frequency at cutoff)
- explain passband (signals shifted to occupy a higher range or frequencies, the case for wireless)
- explain example
    - explain harmonics
    - explain mse and relation with energy
    - explain we want the minimum amount of wave fidelity
- explain other example
    - explain why it makes sense that for higher first harmonics
    it can send more information
    - explain that's why we have to incease harmonics with low frequencies

# Maximum Data Rate of a Channel
- explain that it assumes a perfect channel with no noise or interferance
- explain low-pass filter
- explain sample rate proof
- explain nyquist's theorem (V is # of discrete levels)
- give an example
- explain SNR
- explain that I didn't understand the real formula at first, but the key is that
you can't increase S without increasing N
- explain that the formulas are absolute and come purely from information theory

# Digital Modulation
- explain what is digital modulation
- explain multiplexing

## Baseband Transmission
- explain NRZ

## Bandwidth Efficiency
- explain what efficiency means in this case

## Clock Recovery
- explain that very accurate clocks are very expensive and would need to be very precise
- give the example of trying to keep a beat after a song has stopped playing
- explain that for the physical layer this is kinda it, our clock recovery comes from upper layers
of the OSI model

## Balanced Signals
- explain what are balanced signals (helps with sending through DC components)
- explain 8B / 10B

## Passband Transmission
- explain passband transmission again (used in wifi)
- explain that constellation diagrams mean angle is phase and distance is amplitude
- explain that frequency would interfere with phase

# Multiplexing
- explain what multiplexing is

## FDM
## OFDM
## TDM
## STDM
## CDM / CDMA
- explain that CDM is pretty much always used for everyone using the same
- talk about example in the book about people talking
- explain problem if they aren't talking at the same time
## CDMA
## OFDMA
## WDM
## DWDM

# Extra
- fourier formulas derivation
