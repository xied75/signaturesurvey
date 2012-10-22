#! /usr/bin/python
import signaturesurvey
import sys


def sig2pic(directory):
    signaturesurvey.signature_survey(directory)


if __name__ == "__main__":
    sig2pic(sys.argv[1])
