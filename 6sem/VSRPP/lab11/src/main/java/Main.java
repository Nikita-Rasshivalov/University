import challenges.Challenge;
import challenges.FinalExam;
import challenges.Test;
import list.ChallengeList;

public class Main {
    public static void main(String[] args) {
        Challenge fexam = new FinalExam();
        Challenge test = new Test();
        try {
            while (ChallengeList.hasNext()) {
                System.out.println(ChallengeList.getNext());
            }
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
        }
    }
}
